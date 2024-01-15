using AutoMapper;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Models;
using MagicVilla.MagicApi.Models.DTO;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla.MagicApi.Repository
{
    public class UserRepository : IUserRepository
    {
        internal readonly ApplicationDbContext _db;
        internal readonly IMapper _mapper;
        internal readonly UserManager<ApplicationUser> _userManager;
        internal readonly RoleManager<IdentityRole> _roleManager;
        private string _secret;

        public UserRepository(ApplicationDbContext db, IMapper mapper,
            IConfiguration configuration,UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _secret = configuration.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
            return user == null ? true : false;
        }


        public async Task<TokenDTO> Login(LoginRequestDTO loginRequest)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
            bool isValid =await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (user == null)
            {
                return new TokenDTO
                {
                    AccessToken = "",

                }; ;
            }
            var jwtTokenId = $"JTI{Guid.NewGuid()}";
            var token = await GetAccessToken(user, jwtTokenId);
            var refreshToken = await CreateNewRefreshToken(user.Id, jwtTokenId);
            TokenDTO tokenDTO = new TokenDTO
            {
                AccessToken = token, 
                RefreshToken = refreshToken

            };
            return tokenDTO;

        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequest)
        {
            //ApplicationUser user = _mapper.Map<LocalUser>(registrationRequest);
            ApplicationUser user = new()
            {
                UserName = registrationRequest.UserName,
                PasswordHash = registrationRequest.Password,
                Name = registrationRequest.Name,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequest.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(registrationRequest.Role).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registrationRequest.Role));
                    }
                    await _userManager.AddToRoleAsync(user, registrationRequest.Role);
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequest.UserName);
                    return new UserDTO()
                    {
                        ID=userToReturn.Id,
                        UserName = userToReturn.UserName,
                        Name = userToReturn.Name
                    };
                   
                }
            }catch(Exception ex)
            {

            }
            return new UserDTO();
        }


        private async Task<string> GetAccessToken(ApplicationUser user, string jwtTokenId)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                    new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),


                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       public async Task<TokenDTO> RefreshAccessToken(TokenDTO token)
        {

            var exisitingRefreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(u => u.Refresh_Token == token.RefreshToken);
            if(exisitingRefreshToken == null)
            {
                return new TokenDTO();
            }
            var accessTokenData = GetAccessTokenData(token.AccessToken);
            if (!accessTokenData.isSuccessful || accessTokenData.userId != exisitingRefreshToken.UserId
                || accessTokenData.tokenId != exisitingRefreshToken.JwtTokenId)
            {
                exisitingRefreshToken.IsValid = false;
                _db.SaveChanges();
                return new TokenDTO();

            }
            if (!exisitingRefreshToken.IsValid)
            {
                var chainRecords = _db.RefreshTokens.Where(u => u.UserId == exisitingRefreshToken.UserId && u.JwtTokenId == exisitingRefreshToken.JwtTokenId).ToList();

                foreach (var record in chainRecords)
                {
                    record.IsValid = false;
                }
                _db.UpdateRange(chainRecords);
                _db.SaveChangesAsync();
            }
   
            if (exisitingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                exisitingRefreshToken.IsValid = false;
                _db.SaveChanges();
                return new TokenDTO();

            }
            var newRefreshToken = await CreateNewRefreshToken(exisitingRefreshToken.UserId, exisitingRefreshToken.JwtTokenId);
            exisitingRefreshToken.IsValid = false;
            _db.SaveChanges();
            var applicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == exisitingRefreshToken.UserId);
            if(applicationUser == null)
            {
                return new TokenDTO();
            }
            var newAccessToken = await GetAccessToken(applicationUser, exisitingRefreshToken.JwtTokenId);
            return new TokenDTO()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        private async Task<string> CreateNewRefreshToken(string userId, string tokenId)
        {
            RefreshToken refreshToken = new()
            {
                IsValid = true,
                UserId = userId,
                JwtTokenId = tokenId,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                Refresh_Token = Guid.NewGuid() + "-" + Guid.NewGuid(),
            };
             await _db.RefreshTokens.AddAsync(refreshToken);
           await _db.SaveChangesAsync();
            return refreshToken.Refresh_Token;
                
        }
        private (bool isSuccessful,string userId, string tokenId) GetAccessTokenData(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return (true, userId, jwtTokenId);

            }
            catch (Exception ex)
            {
                return (false, null, null);
            }
        }
    }
}
