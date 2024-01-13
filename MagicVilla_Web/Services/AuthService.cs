using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService :  IAuthService
    {
        public readonly IHttpClientFactory clientFactory;
        private readonly IBaseService _baseService;
        private string VillaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, IBaseService baseService) 
        {

            this.clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
            _baseService = baseService;
        }
        async Task<T> IAuthService.LoginAsync<T>(LoginRequestDTO loginRequest)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequest,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/UsersAuth/login"
            }, withBearer: false);
        }

        async Task<T> IAuthService.RegisterAsync<T>(RegistrationRequestDTO registrationRequest)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequest,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/UsersAuth/register"
            },false);
        }
    }
}
