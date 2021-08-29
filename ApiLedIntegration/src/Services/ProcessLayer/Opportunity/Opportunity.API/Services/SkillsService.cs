using Opportunity.API.Extensions;
using Opportunity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Opportunity.API.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly HttpClient _client;

        public SkillsService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<SkillSetModel> GetSkillSet(string firstName, string lastName, int companyId)
        {
            var response = await _client.GetAsync($"/api/v1/SkillSet/firstName={firstName}&lastName={lastName}&companyId={companyId}");
            return await response.ReadContentAs<SkillSetModel>();
        }
    }
}
