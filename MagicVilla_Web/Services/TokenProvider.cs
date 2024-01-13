using MagicVilla_Utility;
using MagicVilla_Web.Models.DTO;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        void ITokenProvider.ClearToken()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(SD.AccessToken);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(SD.RefreshToken);


        }

        TokenDTO ITokenProvider.GetToken()
        {
            try
            {
                bool hasAccessToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.AccessToken, out string accessToken);
                bool hasRefreshToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.RefreshToken, out string refreshToken);

                return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };

            }catch(Exception ex)
            {
                return null;
            }
        }

        void ITokenProvider.SetToken(TokenDTO token)
        {
            var cookieOptions = new CookieOptions { Expires= DateTime.UtcNow.AddDays(60)};
            _httpContextAccessor.HttpContext.Response.Cookies.Append(SD.AccessToken, token.AccessToken, cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(SD.RefreshToken, token.RefreshToken, cookieOptions);

        }
    }
}
