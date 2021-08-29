using Crm.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.API.Repositories
{
    public interface IOpportunityRepository
    {
        Task<IEnumerable<Opportunity>> GetOpportunities();

        Task<Opportunity> GetOpportunity(int id);

        Task<bool> CreateOpportunity(Opportunity opportunity);

        Task<bool> UpdateOpportunity(Opportunity opportunity);

        Task<bool> DeleteOpportunity(int id);
    }
}
