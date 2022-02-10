using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Web_API.Data.Entities
{
    public class Camp
    {
        public int CampId { get; set; }
        public string Name { get; set; }
        //zkratka
        public string Moniker { get; set; }
        public DateTime EventDate { get; set; }
        public int Length { get; set; }
        public Location Location { get; set; }
    }
}
