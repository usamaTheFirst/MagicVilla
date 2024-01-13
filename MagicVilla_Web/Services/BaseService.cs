using AutoMapper.Internal;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        private readonly ITokenProvider _tokenProvider;
        private readonly string VillaUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        public BaseService(IHttpClientFactory httpClient, ITokenProvider tokenProvider, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.responseModel = new();
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
            this.httpContextAccessor = httpContextAccessor;

        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest, bool withBearer = true)
        {
            try
            {
                var client = _httpClient.CreateClient("MagicAPI");

                var messageFactory = () =>
                {
                    HttpRequestMessage message = new();
                    if (apiRequest.ContentType == SD.ContentType.MultipartFormData)
                    {
                        message.Headers.Add("Accept", "*/*");

                    }
                    else
                    {
                        message.Headers.Add("Accept", "application/json");

                    }
                    message.RequestUri = new Uri(apiRequest.Url);
                    if (apiRequest.ContentType == SD.ContentType.MultipartFormData)
                    {
                        var content = new MultipartFormDataContent();

                        foreach (var prop in apiRequest.Data.GetType().GetProperties())
                        {
                            var value = prop.GetValue(apiRequest.Data);
                            if (value is FormFile)
                            {
                                var file = (FormFile)value;
                                if (file != null)
                                {
                                    content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                                }
                            }
                            else
                            {
                                content.Add(new StringContent(value == null ? "" : value.ToString()), prop.Name);
                            }
                        }
                        message.Content = content;
                    }
                    else
                    {
                        if (apiRequest.Data != null)
                        {
                            message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                                encoding: Encoding.UTF8, "application/json");
                        }
                    }

                    switch (apiRequest.ApiType)
                    {
                        case SD.ApiType.GET:
                            message.Method = HttpMethod.Get;
                            break;
                        case SD.ApiType.POST:
                            message.Method = HttpMethod.Post;
                            break;
                        case SD.ApiType.PUT:
                            message.Method = HttpMethod.Put;
                            break;
                        case SD.ApiType.DELETE:
                            message.Method = HttpMethod.Delete;
                            break;

                    }
                    return message;

                };
                HttpResponseMessage apiResponse = null;
                //if (!string.IsNullOrEmpty(apiRequest.Token))
                if (withBearer && _tokenProvider.GetToken() != null)

                {
                    var token = _tokenProvider.GetToken();
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
                }
                apiResponse = await SendWithRefreshTokenAsync(client, messageFactory, withBearer);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (apiResponse != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest || apiResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObject = JsonConvert.DeserializeObject<T>(res);
                        return returnObject;
                    }
                }
                catch (Exception ex)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;


                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;


            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false

                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;

            }
        }
        private async Task<HttpResponseMessage> SendWithRefreshTokenAsync(HttpClient httpClient,
            Func<HttpRequestMessage> httpRequestMessageFactory, bool withBearer = true)
        {
            if (!withBearer)
            {
                return await httpClient.SendAsync(httpRequestMessageFactory());

            }
            else
            {
                TokenDTO tokenDTO = _tokenProvider.GetToken();
                if (tokenDTO != null && string.IsNullOrEmpty(tokenDTO.AccessToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenDTO.AccessToken);
                }
                try
                {
                    var response = await httpClient.SendAsync(httpRequestMessageFactory());
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    //if fails
                    if(!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await InvokeRefreshTokenEndpoint(httpClient, tokenDTO);
                         response = await httpClient.SendAsync(httpRequestMessageFactory());
                        return response;


                    }

                    return response;


                }
                catch (HttpRequestException ex)
                {
                    if ( ex.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await InvokeRefreshTokenEndpoint(httpClient, tokenDTO);
                      var  response = await httpClient.SendAsync(httpRequestMessageFactory());
                        return response;


                    }
                    throw;
                }
            }
        }
        private async Task InvokeRefreshTokenEndpoint(HttpClient client, TokenDTO tokenDTO)
        {
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri($"{VillaUrl}/api/{SD.CurrentAPIVersion}/UsersAuth/register");
            message.Content = new StringContent(JsonConvert.SerializeObject(tokenDTO),
                                       encoding: Encoding.UTF8, "application/json");
            var response = await client.SendAsync(message);
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<APIResponse>(content);
            if (apiResponse?.IsSuccess != null)
            {
                _tokenProvider.ClearToken();
                await httpContextAccessor.HttpContext.SignOutAsync();
            }
            else
            {
                var tokenDataStr = JsonConvert.SerializeObject(apiResponse.Result);
                var tokenDto = JsonConvert.DeserializeObject<TokenDTO>(tokenDataStr);

                if (tokenDto != null && string.IsNullOrEmpty(tokenDto.AccessToken))
                {
                    //new method to sign in user
                    await SignInWithNewTokens(tokenDto);
                    client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenDTO.AccessToken);

                }
            }
        }
        private async Task SignInWithNewTokens(TokenDTO tokenDTO)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenDTO.AccessToken);

            //HttpContext.Session.SetString(SD.AccessToken, model.AccessToken);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.Name, model.User.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "unique_name").Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
            var principal = new ClaimsPrincipal(identity);
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _tokenProvider.SetToken(tokenDTO);

        }
    }
}
