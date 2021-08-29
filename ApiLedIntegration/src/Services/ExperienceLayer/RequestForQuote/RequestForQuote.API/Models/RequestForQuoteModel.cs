using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestForQuote.API.Models
{
    public class RequestForQuoteModel
    {
        // Fields retrieved from the Opportunity microservice
        public string Customer { get; set; }

        public string RequiredSkills { get; set; }

        public string AccountManager { get; set; }

        public string DeliveryManager { get; set; }

        public string Team { get; set; }

        // Fields set based on data from the Audit microservice
        public DateTime ProposedStartDate { get; set; }

        public DateTime ProposedEndDate { get; set; }

        public double ProposedBudget { get; set; }
    }
}
