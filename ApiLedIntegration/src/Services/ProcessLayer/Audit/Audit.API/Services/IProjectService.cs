using Audit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectModel>> GetProjects();
        Task<ProjectModel> GetProject(int id);
    }
}
