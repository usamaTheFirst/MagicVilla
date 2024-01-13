using MagicVilla_Utility;
using MagicVilla_Web.Model.DTO;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService :  IVillaNumberService
    {
        public readonly IHttpClientFactory clientFactory;
        private readonly IBaseService _baseService;
        private string VillaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration, IBaseService baseService) 
        {

            this.clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
            _baseService = baseService;
        }
        public async Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType =SD.ApiType.POST,
                Data= dto,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/VillaNumberAPI",

            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/VillaNumberAPI" + "/" + id,

            }) ;
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/VillaNumberAPI",

            });
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/VillaNumberAPI/" + id,

            });
        }

        public async Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
        {
            return await _baseService.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = VillaUrl + $"/api/{SD.CurrentAPIVersion}/VillaNumberAPI/" + dto.VillaNo,

            }); 
        }
    }
}
