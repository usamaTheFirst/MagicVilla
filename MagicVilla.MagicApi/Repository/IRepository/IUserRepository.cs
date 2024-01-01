using MagicVilla.MagicApi.Models;
using MagicVilla.MagicApi.Models.DTO;

namespace MagicVilla.MagicApi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequest);


    }
}
