using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Models;
using PeteJourney.API.Repositories;

namespace PeteJourney.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            //var regionsDTO = new List<Models.DTO.Region>();

            /* regions.ToList().ForEach(region=>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id = region.Id,
                    code = region.code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population
                };

                regionsDTO.Add(regionDTO);
            }); */



            return Ok(regionsDTO);
        }
    }
}
