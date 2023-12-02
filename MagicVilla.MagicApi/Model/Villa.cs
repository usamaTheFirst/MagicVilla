using System.ComponentModel.DataAnnotations;

namespace MagicVilla.MagicApi.Model
{
    public class Villa
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
