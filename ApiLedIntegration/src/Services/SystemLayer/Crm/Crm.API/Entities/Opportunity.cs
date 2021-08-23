using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.API.Entities
{
    public class Opportunity
    {
        public int Id { get; set; }

        public string Customer { get; set; }

        public string RequiredSkills { get; set; }

        public int AccountManagerId { get; set; }

        public int DeliveryManagerId { get; set; }
    }
}
