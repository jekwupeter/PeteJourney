using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Models;
using PeteJourney.API.Models.DTO;
using PeteJourney.API.Repositories;

namespace PeteJourney.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [AllowAnonymous]
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
        [Authorize(Roles = "writer, reader")]
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
        [Authorize(Roles = "reader")]
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
            /*if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }*/

            var region = mapper.Map<Models.Domain.Region>(addRegionRequest);

            // pass details to repository
            region = await regionRepository.AddAsync(region);

            //convert back to dto
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { Id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        [Authorize(Roles = "writer")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid Id, [FromBody] Models.DTO.UpdateRegionRequest region)
        {
            //validate input
            if (!ValidateUpdateRegionAsync(region))
            {
                return BadRequest(ModelState);
             }

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

        #region valdiator methods
        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), "Add region data");
                return false;
            }
            if (string.IsNullOrEmpty(addRegionRequest.code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.code), $"{nameof(addRegionRequest.code)} string cannot null");
            }
            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot less zero");
            }
            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long), $"{nameof(addRegionRequest.Long)} cannot be less than zero");
            }
            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat), $"{nameof(addRegionRequest.Lat)} cannot be less than zero");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} cannot be less than zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }


            return true;
        }

        private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegion)
        {
            if (updateRegion == null)
            {
                ModelState.AddModelError(nameof(updateRegion), "Add region data");
                return false;
            }
            if (string.IsNullOrEmpty(updateRegion.code))
            {
                ModelState.AddModelError(nameof(updateRegion.code), $"{nameof(updateRegion.code)} string cannot null");
            }
            if (updateRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Area), $"{nameof(updateRegion.Area)} cannot less zero");
            }
            if (updateRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Long), $"{nameof(updateRegion.Long)} cannot be less than zero");
            }
            if (updateRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Lat), $"{nameof(updateRegion.Lat)} cannot be less than zero");
            }
            if (updateRegion.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Population), $"{nameof(updateRegion.Population)} cannot be less than zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }


            return true;

        }
        #endregion
    }
}
