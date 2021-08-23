using Erp.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetProject(int id);

        Task<bool> CreateProject(Project project);

        Task<bool> UpdateProject(Project project);

        Task<bool> DeleteProject(int id);
    }
}
