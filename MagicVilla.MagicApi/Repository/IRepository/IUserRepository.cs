using MagicVilla.MagicApi.Models;
using MagicVilla.MagicApi.Models.DTO;

namespace MagicVilla.MagicApi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<TokenDTO> Login(LoginRequestDTO loginRequest);
        Task<TokenDTO> RefreshAccessToken(TokenDTO token);

        Task<UserDTO> Register(RegistrationRequestDTO registrationRequest);


    }
}
