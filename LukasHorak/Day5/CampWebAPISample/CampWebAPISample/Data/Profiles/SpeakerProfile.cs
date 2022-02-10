using AutoMapper;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;

namespace CampWebAPISample.Data
{
    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            this.CreateMap<Speaker, SpeakerModel>();
        }
    }
}
