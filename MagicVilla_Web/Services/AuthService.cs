using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService :BaseService,  IAuthService
    {
        public readonly IHttpClientFactory clientFactory;
        private string VillaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {

            this.clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        Task<T> IAuthService.LoginAsync<T>(LoginRequestDTO loginRequest)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequest,
                Url = VillaUrl + "/api/v1/UsersAuth/login"
            });
        }

        Task<T> IAuthService.RegisterAsync<T>(RegistrationRequestDTO registrationRequest)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequest,
                Url = VillaUrl + "/api/v1/UsersAuth/register"
            });
        }
    }
}
