using AutoMapper;
using CampWebAPISample.Data.Entities;
using CampWebAPISample.Models;

namespace CampWebAPISample.Data;

public class CampProfile : Profile
{
    public CampProfile()
    {
        CreateMap<Camp, CampModel>()
            .ReverseMap()
            .ForMember(t => t.Location, opt => opt.Ignore());


        CreateMap<Talk, TalkModel>()
            .ReverseMap()
            .ForMember(t => t.Camp, opt => opt.Ignore())
            .ForMember(t => t.Speaker, opt => opt.Ignore());

        CreateMap<Speaker, SpeakerModel>()
            .ReverseMap();
    }
}