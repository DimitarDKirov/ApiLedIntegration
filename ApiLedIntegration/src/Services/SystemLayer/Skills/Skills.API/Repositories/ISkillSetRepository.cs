using Skills.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skills.API.Repositories
{
    public interface ISkillSetRepository
    {
        Task<SkillSet> GetSkillSet(int companyId);
        Task<SkillSet> UpdateSkillSet(SkillSet skillSet);
        Task DeleteSkillSet(int companyId);
    }
}
