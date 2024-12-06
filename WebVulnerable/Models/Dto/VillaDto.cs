using System.ComponentModel.DataAnnotations;

namespace WebVulnerable.Data.Dto
{
    public class VillaDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }

        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public int Sqft {get; set; }
    }
}
