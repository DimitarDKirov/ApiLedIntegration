using Audit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audit.API.Services
{
    public interface IOpportunityService
    {
        Task<IEnumerable<OpportunityModel>> GetOpportunities();
        Task<OpportunityModel> GetOpportunity(int id);
    }
}
