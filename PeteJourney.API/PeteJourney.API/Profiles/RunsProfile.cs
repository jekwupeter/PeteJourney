using AutoMapper;
using AutoMapper.Execution;

namespace PeteJourney.API.Profiles
{
    public class RunsProfile : Profile
    {
        public RunsProfile()
        {
            CreateMap<Models.Domain.Run, Models.DTO.Run>();

            CreateMap<Models.Domain.RunDifficulty, Models.DTO.RunDifficulty>();

            CreateMap<Models.DTO.Run, Models.Domain.Run>();

            CreateMap<Models.Domain.Run, Models.DTO.AddRunRequest>()
                .ReverseMap()
                .ForMember(x => x.Id, act=>act.Ignore())
                .ForMember(x => x.RunDifficulty, act=>act.Ignore())
                .ForMember(x => x.Region, act=>act.Ignore());

            CreateMap<Models.Domain.Run, Models.DTO.UpdateRunRequest>()
                .ReverseMap()
                .ForMember(x => x.Id, act => act.Ignore())
                .ForMember(x => x.RunDifficulty, act => act.Ignore())
                .ForMember(x => x.Region, act => act.Ignore());
                
        }
    }
}
