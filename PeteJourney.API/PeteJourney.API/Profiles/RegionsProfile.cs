using AutoMapper;
using AutoMapper.Execution;

namespace PeteJourney.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
            CreateMap<Models.DTO.Region, Models.Domain.Region>();
            CreateMap<Models.DTO.AddRegionRequest, Models.Domain.Region>()
                .ForMember(x => x.Id, act=>act.Ignore())
                .ReverseMap();
            CreateMap<Models.DTO.UpdateRegionRequest, Models.Domain.Region>()
                .ForMember(x => x.Id, act => act.Ignore())
                .ReverseMap();
            CreateMap<Models.DTO.UpdateRegionRequest, Models.Domain.Region>();

        }
    }
}
