using AutoMapper;

namespace PeteJourney.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
        }
    }
}
