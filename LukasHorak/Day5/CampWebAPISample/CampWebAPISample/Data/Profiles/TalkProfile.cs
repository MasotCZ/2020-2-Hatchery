using AutoMapper;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;

namespace CampWebAPISample.Data
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            this.CreateMap<Talk, TalkModel>();
        }
    }
}
