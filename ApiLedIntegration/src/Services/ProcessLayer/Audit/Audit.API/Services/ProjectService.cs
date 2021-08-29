using Audit.API.Extensions;
using Audit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Audit.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _client;

        public ProjectService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            var response = await _client.GetAsync($"/api/v1/Project");
            return await response.ReadContentAs<List<ProjectModel>>();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            var response = await _client.GetAsync($"/api/v1/Project/{id}");
            return await response.ReadContentAs<ProjectModel>();
        }
    }
}
