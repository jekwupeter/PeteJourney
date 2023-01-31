using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PeteJourney.API.Models;
using PeteJourney.API.Models.DTO;
using PeteJourney.API.Repositories;

namespace PeteJourney.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunsController : Controller
    {
        private readonly IRunRepository runRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IRunDifficultyRepository runDifficultyRepository;

        public RunsController(IRunRepository runRepository, IMapper mapper, IRegionRepository regionRepository, IRunDifficultyRepository runDifficultyRepository)
        {
            this.runRepository = runRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.runDifficultyRepository = runDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var runsDomain = await runRepository.GetAllAsync();

            var runsDTO = mapper.Map<List<Models.DTO.Run>>(runsDomain);

            return Ok(runsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRunAsync")]
        public async Task<IActionResult> GetRunAsync(Guid id)
        {
            var run = await runRepository.GetAsync(id);

            if (run == null)
            {
                return NotFound();
            }

            var runDTO = mapper.Map<Models.DTO.Run>(run);

            return Ok(runDTO);

        }

        [HttpPost]
        public async Task<IActionResult> AddRunAsync(Models.DTO.AddRunRequest addRunRequest)
        {
            // validate incoming data
            if (!await ValidateAddRunAsync(addRunRequest))
            {
                return BadRequest(ModelState);
            }
            // request to domain model

            var run = mapper.Map<Models.Domain.Run>(addRunRequest);

            // pass details to repository
            run = await runRepository.AddAsync(run);

            //convert back to dto
            var runDTO = mapper.Map<Models.DTO.Run>(run);

            return CreatedAtAction(nameof(GetRunAsync), new { Id = runDTO.Id }, runDTO);
        }

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> RunAsync(Guid Id)
        {
            // get region frm db
            var deletedRun = await runRepository.DeleteAsync(Id);

            // check for null
            if (deletedRun == null)
            {
                return NotFound();
            }

            // cponvert to dto

            Models.DTO.Run runDTO = mapper.Map<Models.DTO.Run>(deletedRun);

            // return ok
            return Ok(runDTO);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateRunAsync([FromRoute] Guid Id, [FromBody] Models.DTO.UpdateRunRequest run)
        {
            // validate 
            if (!await ValidateUpdateRunAsync(run))
            {
                return BadRequest(ModelState);
            }

            // convert to domain model
            var runToUpdate = mapper.Map<Models.Domain.Run>(run);

            // update
            var updatedRun = await runRepository.UpdateAsync(Id, runToUpdate);

            // check for null
            if (updatedRun == null)
            {
                return NotFound();
            }

            // convert to dto
            var updatedRunDTO = mapper.Map<Models.DTO.Run>(updatedRun);

            // return ok
            return Ok(updatedRun);
        }

        #region validate methods
        private async Task<bool> ValidateAddRunAsync(Models.DTO.AddRunRequest addRunRequest)
        {
            /*
            if (addRunRequest == null)
            {
                ModelState.AddModelError(nameof(addRunRequest), $"{nameof(addRunRequest)} cannot be empty");
                return false;
            }

            if (string.IsNullOrEmpty(addRunRequest.Name))

            {
                ModelState.AddModelError(nameof(addRunRequest.Name), $"{nameof(addRunRequest.Name)} cannot be empty");
                return false;
            }

            if (addRunRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(addRunRequest.Length), $"{nameof(addRunRequest.Length)} cannot be empty");
                return false;
            }
            */

            Models.Domain.Region? region = await regionRepository.GetAsync(addRunRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(addRunRequest.RegionId), $"{addRunRequest.RegionId} is not valid");
                
            }

            var runDifficultyId = await runDifficultyRepository.GetAsync(addRunRequest.RunDifficultyId);

            if (runDifficultyId == null)
            {
                ModelState.AddModelError(nameof(addRunRequest.RunDifficultyId), $"{addRunRequest.RunDifficultyId} is not valid");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateRunAsync(Models.DTO.UpdateRunRequest updateRunRequest)
        {/*
            if (updateRunRequest == null)
            {
                ModelState.AddModelError(nameof(updateRunRequest), $"{nameof(updateRunRequest)} cannot be empty");
                return false;
            }

            if (string.IsNullOrEmpty(updateRunRequest.Name))

            {
                ModelState.AddModelError(nameof(updateRunRequest.Name), $"{nameof(updateRunRequest.Name)} cannot be empty");
            }

            if (updateRunRequest.Length < 0)
            {
                ModelState.AddModelError(nameof(updateRunRequest.Length), $"{nameof(updateRunRequest.Length)} cannot be empty");
            }
            */

            Models.Domain.Region? region = await regionRepository.GetAsync(updateRunRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(updateRunRequest.RegionId), $"{updateRunRequest.RegionId} is not valid");

            }

            var runDifficultyId = await runDifficultyRepository.GetAsync(updateRunRequest.RunDifficultyId);

            if (runDifficultyId == null)
            {
                ModelState.AddModelError(nameof(updateRunRequest.RunDifficultyId), $"{updateRunRequest.RunDifficultyId} is not valid");

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
