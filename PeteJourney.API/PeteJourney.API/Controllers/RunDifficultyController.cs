using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Models;
using PeteJourney.API.Profiles;
using PeteJourney.API.Repositories;

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
        public async Task<IActionResult> GetAllRunDifficultyAsync()
        {
            var runDifficulties = await rdRepository.GetAllAsync();

            var rdDTO = mapper.Map<List<Models.DTO.RunDifficulty>>(runDifficulties);

            return Ok(rdDTO);
        }
        
        [HttpGet]
        [Route("{id:guid}")]
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
        public async Task<IActionResult> AddRunDifficultyAsync(Models.DTO.AddRunDifficultyRequest addRunDifficultyRequest)
        {
            // request to domain model
            
            var rd = mapper.Map<Models.Domain.RunDifficulty>(addRunDifficultyRequest);

            // pass details to repository
            rd = await rdRepository.AddAsync(rd);

            //convert back to dto
            var rdDTO = mapper.Map<Models.DTO.RunDifficulty>(rd);

            return CreatedAtAction(nameof(GetRunDifficultyAsync), new { Id = rdDTO.Id}, rdDTO);
        }
        
        [HttpDelete]
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
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid Id, [FromBody]Models.DTO.UpdateRunDifficultyRequest updateRunDifficultyRequest)
        {
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
    }
}
