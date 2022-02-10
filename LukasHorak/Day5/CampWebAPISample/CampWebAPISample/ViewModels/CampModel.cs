using System.ComponentModel.DataAnnotations;

namespace CampWebAPISample.Models
{
    public class CampModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; }
        public LocationModel Location { get; set; }
    }
}
