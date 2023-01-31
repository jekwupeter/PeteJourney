using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeteJourney.API.Models;
using PeteJourney.API.Repositories;

namespace PeteJourney.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunsController : Controller
    {
        private readonly IRunRepository runRepository;
        private readonly IMapper mapper;

        public RunsController(IRunRepository runRepository, IMapper mapper)
        {
            this.runRepository = runRepository;
            this.mapper = mapper;
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
            // request to domain model
            
            var run = mapper.Map<Models.Domain.Run>(addRunRequest);

            // pass details to repository
            run = await runRepository.AddAsync(run);

            //convert back to dto
            var runDTO = mapper.Map<Models.DTO.Run>(run);

            return CreatedAtAction(nameof(GetRunAsync), new { Id = runDTO.Id}, runDTO);
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
        public async Task<IActionResult> UpdateRunAsync([FromRoute]Guid Id, [FromBody]Models.DTO.UpdateRunRequest run)
        {
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
    }
}
