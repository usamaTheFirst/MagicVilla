﻿using MagicVilla_Utility;
using MagicVilla_Web.Model.DTO;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        public readonly IHttpClientFactory clientFactory;
        private string VillaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory, IConfiguration configuration) :base(clientFactory)
        {

            this.clientFactory = clientFactory;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType =SD.ApiType.POST,
                Data= dto,
                Url = VillaUrl + "/api/v1/VillaNumberAPI",
                Token = token

            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = VillaUrl + "/api/v1/VillaNumberAPI" + "/" + id,
                Token = token

            }) ;
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrl + "/api/v1/VillaNumberAPI",
                Token = token

            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = VillaUrl + "/api/v1/VillaNumberAPI/" + id,
                Token = token

            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = VillaUrl + "/api/v1/VillaNumberAPI/" + dto.VillaNo,
                Token = token

            }); 
        }
    }
}
