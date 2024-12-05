using System.ComponentModel.DataAnnotations;

namespace WebVulnerable.Data.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Occupancy { get; set; }

        public int Sqft {get; set; }
    }
}
