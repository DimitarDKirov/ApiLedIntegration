using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opportunity.API.Models
{
    public class QuoteModel
    {
        public string Customer { get; set; }

        public string RequiredSkills { get; set; }

        public string AccountManager { get; set; }

        public string DeliveryManager { get; set; }

        public string Team { get; set; }
    }
}
