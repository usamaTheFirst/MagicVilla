using AutoMapper;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Models;
using MagicVilla.MagicApi.Models.DTO;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
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


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.UserName.ToLower());
            bool isValid =await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (user == null)
            {
                return new LoginResponseDTO
                {
                    Token = "",
                    User = null

                }; ;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponse = new LoginResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user),

            };
            return loginResponse;

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
    }
}
