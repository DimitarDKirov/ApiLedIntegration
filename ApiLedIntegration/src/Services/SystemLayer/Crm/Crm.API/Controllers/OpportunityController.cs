using Crm.API.Entities;
using Crm.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Crm.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OpportunityController : ControllerBase
    {
        private readonly IOpportunityRepository _repository;

        public OpportunityController(IOpportunityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{id}", Name ="GetOpportunity")]
        [ProducesResponseType(typeof(Opportunity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Opportunity>> GetOpportunity(int id)
        {
            var opportunity = await _repository.GetOpportunity(id);
            return Ok(opportunity);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Opportunity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Opportunity>> CreateOpportunity([FromBody] Opportunity opportunity)
        {
            await _repository.CreateOpportunity(opportunity);
            return CreatedAtRoute("GetOpportunity", new { id = opportunity.Id }, opportunity);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Opportunity), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Opportunity>> UpdateOpportunity([FromBody] Opportunity opportunity)
        {
            return Ok(await _repository.UpdateOpportunity(opportunity));
        }

        [HttpDelete("{id}", Name = "DeleteOpportunity")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteOpportunity(int id)
        {
            return Ok(await _repository.DeleteOpportunity(id));
        }
    }
}
