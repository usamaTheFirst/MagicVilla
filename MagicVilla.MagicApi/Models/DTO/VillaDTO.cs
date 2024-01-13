using System.ComponentModel.DataAnnotations;

namespace MagicVilla.MagicApi.Model.DTO
{
    public class VillaDTO
    {


        public int Id { get; set; }
        [Required]
            [MaxLength(30)]
            public string Name { get; set; }
            public string Details { get; set; }
            [Required]
            public double Rate { get; set; }
            public int Sqft { get; set; }
            public int Occupancy { get; set; }
        public IFormFile? Image { get; set; }
            public string ImageURL { get; set; }
        public string Amenities { get; set; }
        }
    }



