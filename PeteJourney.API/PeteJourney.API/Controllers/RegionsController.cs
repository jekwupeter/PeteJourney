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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // request to domain model
            /*
            var region = new Models.Domain.Region()
            {
                code = addRegionRequest.code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            }; */
            var region = mapper.Map<Models.Domain.Region>(addRegionRequest);

            // pass details to repository
            region = await regionRepository.AddAsync(region);

            //convert back to dto
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { Id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid Id)
        {
            // get region frm db
            var deletedRegion = await regionRepository.DeleteAsync(Id);

            // check for null
            if (deletedRegion == null)
            {
                return NotFound();
            }

            // cponvert to dto
            
            Models.DTO.Region regionDTO = mapper.Map<Models.DTO.Region>(deletedRegion);
            
            // return ok
            return Ok(deletedRegion);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid Id, [FromBody]Models.DTO.UpdateRegionRequest region)
        {
            // convert to domain model
            var regionToUpdate = mapper.Map<Models.Domain.Region>(region);

            // update
            var updatedRegion = await regionRepository.UpdateAsync(Id, regionToUpdate);

            // check for null
            if (updatedRegion == null)
            {
                return NotFound();
            }

            // convert to dto
            var updatedRegionDTO = mapper.Map<Models.DTO.UpdateRegionRequest>(updatedRegion);

            // return ok
            return Ok(updatedRegion);
        }
    }
}
