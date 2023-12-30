using AutoMapper;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Models;
using MagicVilla.MagicApi.Models.DTO;
using MagicVilla.MagicApi.Repository.IRepository;
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
        private string _secret;

        public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _secret = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
            return user == null ? true : false;
        }


        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.Username.ToLower() == loginRequest.Username.ToLower()
            && u.Password == loginRequest.Password);

            if (user == null)
            {
                return new LoginResponseDTO
                {
                    Token = "",
                    LocalUser = null

                }; ;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponse = new LoginResponseDTO
            {
                Token = tokenHandler.WriteToken(token),
                LocalUser = user

            };
            return loginResponse;

        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequest)
        {
            LocalUser user = _mapper.Map<LocalUser>(registrationRequest);
            _db.LocalUsers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
