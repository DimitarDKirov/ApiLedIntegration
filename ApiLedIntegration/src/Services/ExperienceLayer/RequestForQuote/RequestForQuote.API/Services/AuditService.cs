using RequestForQuote.API.Extensions;
using RequestForQuote.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestForQuote.API.Services
{
    public class AuditService : IAuditService
    {
        private readonly HttpClient _client;

        public AuditService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ReportModel> GetReport()
        {
            var response = await _client.GetAsync($"/api/v1/Report");
            return await response.ReadContentAs<ReportModel>();
        }
    }
}
