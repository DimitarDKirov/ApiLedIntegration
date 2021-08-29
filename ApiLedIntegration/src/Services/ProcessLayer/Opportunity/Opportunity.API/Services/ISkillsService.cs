using Opportunity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opportunity.API.Services
{
    public interface ISkillsService
    {
        Task<SkillSetModel> GetSkillSet(string firstName, string lastName, int companyId);
    }
}
