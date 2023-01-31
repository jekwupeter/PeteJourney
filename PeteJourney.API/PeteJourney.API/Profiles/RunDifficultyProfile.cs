using AutoMapper;

namespace PeteJourney.API.Profiles
{
    public class RunDifficultyProfile : Profile
    {
        public RunDifficultyProfile()
        {
            CreateMap<Models.Domain.RunDifficulty, Models.DTO.RunDifficulty>();
            
            CreateMap<Models.DTO.AddRunDifficultyRequest, Models.Domain.RunDifficulty>()
                .ForMember(x => x.Id, act => act.Ignore())
                .ReverseMap();

            CreateMap<Models.DTO.UpdateRunDifficultyRequest, Models.Domain.RunDifficulty>()
                .ForMember(x => x.Id, act => act.Ignore())
                .ReverseMap();
        }
        
    }
}
