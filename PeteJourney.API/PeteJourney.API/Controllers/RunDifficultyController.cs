using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Models;
using PeteJourney.API.Models.DTO;
using PeteJourney.API.Profiles;
using PeteJourney.API.Repositories;
using System.Data;

namespace PeteJourney.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunDifficultyController : Controller
    {
        private readonly IRunDifficultyRepository rdRepository;
        private readonly IMapper mapper;

        public RunDifficultyController(IRunDifficultyRepository runDifficultyRepository, IMapper mapper)
        {
            this.rdRepository = runDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "writer, reader")]
        public async Task<IActionResult> GetAllRunDifficultyAsync()
        {
            var runDifficulties = await rdRepository.GetAllAsync();

            var rdDTO = mapper.Map<List<Models.DTO.RunDifficulty>>(runDifficulties);

            return Ok(rdDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer, reader")]
        [ActionName("GetRunDifficultyAsync")]
        public async Task<IActionResult> GetRunDifficultyAsync(Guid id)
        {
            var rd = await rdRepository.GetAsync(id);

            if (rd == null)
            {
                return NotFound();
            }

            var runDifficultyDTO = mapper.Map<Models.DTO.RunDifficulty>(rd);

            return Ok(runDifficultyDTO);

        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRunDifficultyAsync(Models.DTO.AddRunDifficultyRequest addRunDifficultyRequest)
        {
            /*
            // validate input
            if (!ValidateAddRDAsync(addRunDifficultyRequest))
            {
                return BadRequest(ModelState);
            }*/

            // request to domain model

            var rd = mapper.Map<Models.Domain.RunDifficulty>(addRunDifficultyRequest);

            // pass details to repository
            rd = await rdRepository.AddAsync(rd);

            //convert back to dto
            var rdDTO = mapper.Map<Models.DTO.RunDifficulty>(rd);

            return CreatedAtAction(nameof(GetRunDifficultyAsync), new { Id = rdDTO.Id }, rdDTO);
        }

        [HttpDelete]
        [Authorize(Roles = "writer")]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteRunDifficultyAsync(Guid Id)
        {
            // get region frm db
            var deletedRD = await rdRepository.DeleteAsync(Id);

            // check for null
            if (deletedRD == null)
            {
                return NotFound();
            }

            // cponvert to dto

            Models.DTO.RunDifficulty rdDTO = mapper.Map<Models.DTO.RunDifficulty>(deletedRD);

            // return ok
            return Ok(deletedRD);
        }

        [HttpPut]
        [Authorize(Roles = "writer")]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateRunDifficultyAsync([FromRoute] Guid Id, [FromBody] Models.DTO.UpdateRunDifficultyRequest updateRunDifficultyRequest)
        {
            /*
            // validate input
            if (!ValidateUpdateRDAsync(updateRunDifficultyRequest))
            {
                return BadRequest(ModelState);

            }
            */

            // convert to domain model
            var rdToUpdate = mapper.Map<Models.Domain.RunDifficulty>(updateRunDifficultyRequest);

            // update
            var updatedRD = await rdRepository.UpdateAsync(Id, rdToUpdate);

            // check for null
            if (updatedRD == null)
            {
                return NotFound();
            }

            // convert to dto
            var updatedRDDTO = mapper.Map<Models.DTO.RunDifficulty>(updatedRD);

            // return ok
            return Ok(updatedRDDTO);
        }

        #region validation methods
        private bool ValidateAddRDAsync(Models.DTO.AddRunDifficultyRequest addRunDifficultyRequest)
        {
            if (addRunDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addRunDifficultyRequest), $"{nameof(addRunDifficultyRequest)} canont be null");
                return false;
            }

            if (string.IsNullOrEmpty(addRunDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addRunDifficultyRequest.Code), $"{nameof(addRunDifficultyRequest.Code)} canont be null");
                return false;
            }

            return true;
        }
        private bool ValidateUpdateRDAsync(Models.DTO.UpdateRunDifficultyRequest updateRunDifficultyRequest)
        {
            if (updateRunDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateRunDifficultyRequest), $"{nameof(updateRunDifficultyRequest)} canont be null");

                return false;
            }

            if (string.IsNullOrEmpty(updateRunDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRunDifficultyRequest.Code), $"{nameof(updateRunDifficultyRequest.Code)} canont be null");
                return false;
            }

            return true;
        }
        #endregion
    }
}
