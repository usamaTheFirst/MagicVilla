using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Services.IServices
{
    public interface ITokenProvider
    {
        void SetToken(TokenDTO token);
        TokenDTO? GetToken();
        void ClearToken();
    }
}
