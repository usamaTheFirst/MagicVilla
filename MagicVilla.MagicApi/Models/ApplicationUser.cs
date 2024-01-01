using Microsoft.AspNetCore.Identity;

namespace MagicVilla.MagicApi.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Name { get; set; }
    }
}
