using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opportunity.API.Models
{
    public class SkillSetModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompanyId { get; set; }

        public List<SkillItemModel> Items { get; set; } = new List<SkillItemModel>();
    }
}
