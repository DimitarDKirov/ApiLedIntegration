using Opportunity.API.Extensions;
using Opportunity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Opportunity.API.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly HttpClient _client;

        public OpportunityService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OpportunityModel>> GetOpportunities()
        {
            var response = await _client.GetAsync($"/api/v1/Opportunity");
            return await response.ReadContentAs<List<OpportunityModel>>();
        }

        public async Task<OpportunityModel> GetOpportunity(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Opportunity/{id}");
            return await response.ReadContentAs<OpportunityModel>();
        }
    }
}
