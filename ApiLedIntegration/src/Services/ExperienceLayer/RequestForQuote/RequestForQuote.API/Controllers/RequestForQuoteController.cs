using Microsoft.AspNetCore.Mvc;
using RequestForQuote.API.Models;
using RequestForQuote.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RequestForQuote.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RequestForQuoteController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        private readonly IAuditService _auditService;

        public RequestForQuoteController(IOpportunityService opportunityService, IAuditService auditService)
        {
            _opportunityService = opportunityService ?? throw new ArgumentNullException(nameof(opportunityService));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
        }

        [HttpGet("customerName={customerName}&department={department}&requiredSkills={requiredSkills}", Name = "GetRequestForQuote")]
        [ProducesResponseType(typeof(RequestForQuoteModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestForQuoteModel>> GetRequestForQuote(string customerName, string department, string requiredSkills)
        {
            var quote = await _opportunityService.GetQuote(customerName, department, requiredSkills);
            var report = await _auditService.GetReport();

            var nextMonday = GetNextMonday();
            var averageStatsForDepartment = GetAverageStatsForDepartment(department, report);

            var requestForQuote = new RequestForQuoteModel
            {
                Customer = customerName,
                AccountManager = quote.AccountManager,
                DeliveryManager = quote.DeliveryManager,
                RequiredSkills = requiredSkills,
                Team = quote.Team,
                ProposedStartDate = nextMonday,
                ProposedEndDate = nextMonday.AddDays(averageStatsForDepartment.Item1), // Next Monday + the average length to complete a project for the department
                ProposedBudget = averageStatsForDepartment.Item2 // The average budget of a project for the deparment
            };

            return Ok(requestForQuote);
        }

        private DateTime GetNextMonday()
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)tomorrow.DayOfWeek + 7) % 7;
            return tomorrow.AddDays(daysUntilMonday);
        }

        private Tuple<int, double> GetAverageStatsForDepartment(string department, ReportModel report)
        {
            switch(department)
            {
                case ".NET": return Tuple.Create(report.AverageDaysToFinishNet, report.AverageBudgetForNet);
                case "Java": return Tuple.Create(report.AverageDaysToFinishJava, report.AverageBudgetForJava);
                case "PHP": return Tuple.Create(report.AverageDaysToFinishPhp, report.AverageBudgetForPhp);
                case "UX": return Tuple.Create(report.AverageDaysToFinishUx, report.AverageBudgetForUx);
                default: return null;
            }
        }
    }
}
