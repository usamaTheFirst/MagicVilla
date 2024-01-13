using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Model.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageURL { get; set; }
        public IFormFile? Image { set; get; }
        public string Amenities { get; set; }
    }
}
