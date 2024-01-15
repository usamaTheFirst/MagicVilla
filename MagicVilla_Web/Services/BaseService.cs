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
        private readonly IApiMessageRequestBuilder messageRequestBuilder;
        public BaseService(IHttpClientFactory httpClient, ITokenProvider tokenProvider, IConfiguration configuration, IHttpContextAccessor httpContextAccessor,IApiMessageRequestBuilder requestBuilder)
        {
            this.responseModel = new();
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
            VillaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
            this.httpContextAccessor = httpContextAccessor;
            messageRequestBuilder = requestBuilder;

        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest, bool withBearer = true)
        {
            try
            {
                var client = _httpClient.CreateClient("MagicAPI");

                var messageFactory = () =>
                {
                 return   messageRequestBuilder.Build(apiRequest);
                };
                HttpResponseMessage httpResponseMessage = null;
                APIResponse finalApiResponse = new()
                {
                    IsSuccess=false
                };
                ////if (!string.IsNullOrEmpty(apiRequest.Token))
                //if (withBearer && _tokenProvider.GetToken() != null)

                //{
                //    var token = _tokenProvider.GetToken();
                //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
                //}
                httpResponseMessage = await SendWithRefreshTokenAsync(client, messageFactory, withBearer);
                //var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();

                try
                {
                    switch (httpResponseMessage.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            finalApiResponse.ErrorMessages = new List<string>() { "Not Found"};
                            break;
                        case HttpStatusCode.Forbidden:
                            finalApiResponse.ErrorMessages = new List<string>() { "Access Denied" };
                            break;
                        case HttpStatusCode.Unauthorized:
                            finalApiResponse.ErrorMessages = new List<string>() { "Unauthorized" };
                            break;
                        case HttpStatusCode.InternalServerError:
                            finalApiResponse.ErrorMessages = new List<string>() { "Internal Server Error" };
                            break;
                        default:
                            var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                            finalApiResponse.IsSuccess = true;
                            finalApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                            break;


                    }
                  


                    //if (httpResponseMessage != null && (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest || httpResponseMessage.StatusCode == HttpStatusCode.NotFound))
                    //{
                    //    ApiResponse.StatusCode = HttpStatusCode.BadRequest;
                    //    ApiResponse.IsSuccess = false;
                    //    var res = JsonConvert.SerializeObject(ApiResponse);
                    //    var returnObject = JsonConvert.DeserializeObject<T>(res);
                    //    return returnObject;
                    //}
                }
                catch (Exception ex)
                {
                    finalApiResponse.ErrorMessages = new List<string>() { "Error Encountered",ex.Message.ToString()};


                }
                var res = JsonConvert.SerializeObject(finalApiResponse);
                var returnObject = JsonConvert.DeserializeObject<T>(res);
                return returnObject;
             


            }
            catch(AuthException )
            {
                throw;
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


                }catch(AuthException )
                {
                    throw ;
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
            if (apiResponse.IsSuccess)
            {
                _tokenProvider.ClearToken();
                await httpContextAccessor.HttpContext.SignOutAsync();
                throw new AuthException();
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
