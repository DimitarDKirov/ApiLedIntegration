using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opportunity.API.Models
{
    public class OpportunityModel
    {
        public int Id { get; set; }

        public string Customer { get; set; }

        public string RequiredSkills { get; set; }

        public int AccountManagerId { get; set; }

        public int DeliveryManagerId { get; set; }

        public string Department { get; set; }
    }
}
