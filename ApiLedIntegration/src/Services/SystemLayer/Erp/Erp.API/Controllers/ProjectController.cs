using Erp.API.Entities;
using Erp.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Erp.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _repository;

        public ProjectController(IProjectRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetProjects")]
        [ProducesResponseType(typeof(IEnumerable<Project>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var projects = await _repository.GetProjects();
            return Ok(projects);
        }

        [HttpGet("{id}", Name = "GetProject")]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _repository.GetProject(id);
            return Ok(project);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
        {
            await _repository.CreateProject(project);
            return CreatedAtRoute("GetProject", new { id = project.Id }, project);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Project), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Project>> UpdateProject([FromBody] Project project)
        {
            return Ok(await _repository.UpdateProject(project));
        }

        [HttpDelete("{id}", Name = "DeleteProject")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProject(int id)
        {
            return Ok(await _repository.DeleteProject(id));
        }
    }
}
