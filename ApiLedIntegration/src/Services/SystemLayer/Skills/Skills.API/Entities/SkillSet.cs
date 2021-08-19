using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skills.API.Entities
{
    public class SkillSet
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompanyId { get; set; }

        public List<SkillItem> Items { get; set; } = new List<SkillItem>();

        public SkillSet()
        {

        }

        public SkillSet(string firstName, string lastName, int companyId)
        {

        }
    }
}
