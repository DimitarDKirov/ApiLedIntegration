using RequestForQuote.API.Extensions;
using RequestForQuote.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestForQuote.API.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly HttpClient _client;

        public OpportunityService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<QuoteModel> GetQuote(string customerName, string department, string requiredSkills)
        {
            var response = await _client.GetAsync($"/api/v1/Quote/customerName={customerName}&department={department}&requiredSkills={requiredSkills}");
            return await response.ReadContentAs<QuoteModel>();
        }
    }
}
