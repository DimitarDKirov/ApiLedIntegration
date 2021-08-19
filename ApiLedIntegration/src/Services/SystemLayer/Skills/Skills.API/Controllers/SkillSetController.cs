using Microsoft.AspNetCore.Mvc;
using Skills.API.Entities;
using Skills.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Skills.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillSetController : ControllerBase
    {
        private readonly ISkillSetRepository _repository;

        public SkillSetController(ISkillSetRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("firstName={firstName}&lastName={lastName}&companyId={companyId}", Name = "GetSkillSet")]
        [ProducesResponseType(typeof(SkillSet), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SkillSet>> GetSkillSet(string firstName, string lastName, int companyId)
        {
            var skillSet = await _repository.GetSkillSet(companyId);
            return Ok(skillSet ?? new SkillSet(firstName, lastName, companyId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SkillSet), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SkillSet>> UpdateSkillSet([FromBody] SkillSet skillSet)
        {
            return Ok(await _repository.UpdateSkillSet(skillSet));
        }

        [HttpDelete("{companyId}", Name = "DeleteSkillSet")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSkillSet(int companyId)
        {
            await _repository.DeleteSkillSet(companyId);
            return Ok();
        }
    }
}
