namespace CampWebAPISample.Models
{
    public class CampModel
    {
        public string Name { get; set; }
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; }
        public LocationModel Location { get; set; }
    }
}
