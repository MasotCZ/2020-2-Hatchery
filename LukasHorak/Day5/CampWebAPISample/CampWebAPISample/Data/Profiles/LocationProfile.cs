using AutoMapper;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;

namespace CampWebAPISample.Data
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            this.CreateMap<Location, LocationModel>();
        }
    }
}
