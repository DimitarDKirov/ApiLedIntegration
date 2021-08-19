using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Skills.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skills.API.Repositories
{
    public class SkillSetRepository : ISkillSetRepository
    {
        private readonly IDistributedCache _redisCache;

        public SkillSetRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<SkillSet> GetSkillSet(int companyId)
        {
            var skillSet = await _redisCache.GetStringAsync(companyId.ToString());
            
            if (String.IsNullOrEmpty(skillSet))
                return null;

            return JsonConvert.DeserializeObject<SkillSet>(skillSet);
        }

        public async Task<SkillSet> UpdateSkillSet(SkillSet skillSet)
        {
            await _redisCache.SetStringAsync(skillSet.CompanyId.ToString(), JsonConvert.SerializeObject(skillSet));

            return await GetSkillSet(skillSet.CompanyId);
        }

        public async Task DeleteSkillSet(int companyId)
        {
            await _redisCache.RemoveAsync(companyId.ToString());
        }
    }
}
