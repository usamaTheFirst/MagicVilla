namespace MagicVilla_Web.Models.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO LocalUser { get; set; }
        public string Token { get; set; }
    }
}
