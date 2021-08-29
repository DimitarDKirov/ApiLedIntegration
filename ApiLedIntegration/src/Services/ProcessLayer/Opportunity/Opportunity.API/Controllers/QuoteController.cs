using Microsoft.AspNetCore.Mvc;
using Opportunity.API.Models;
using Opportunity.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        private readonly ISkillsService _skillsService;
        private readonly IEmployeeService _employeeService;

        public QuoteController(IOpportunityService opportunityService, ISkillsService skillsService, IEmployeeService employeeService)
        {
            _opportunityService = opportunityService ?? throw new ArgumentNullException(nameof(opportunityService));
            _skillsService = skillsService ?? throw new ArgumentNullException(nameof(skillsService));
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpGet("customerName={customerName}&department={department}&requiredSkills={requiredSkills}", Name = "GetQuote")]
        [ProducesResponseType(typeof(QuoteModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<QuoteModel>> GetQuote(string customerName, string department, string requiredSkills)
        {
            // Retrieving the employees for the given department
            var employees = await _employeeService.GetEmployees();

            // Retrieving the opportunities for the given department
            var opportunitiesForDepartment = (await _opportunityService.GetOpportunities()).Where(x => x.Department == department).ToList();

            // Searching for an underloaded (handling less than 5 opportunities) Account Manager to be assigned to the quote
            var underloadedAccountManager = employees.Where(x => x.Role == "Account Manager" && opportunitiesForDepartment.Where(y => y.AccountManagerId == x.CompanyId).Count() < 5).FirstOrDefault();

            // Searching for an underloaded (handling less than 5 opportunities) Delivery Manager to be assigned to the quote
            var underloadedDeliveryManager = employees.Where(x => x.Role == "Delivery Manager" && opportunitiesForDepartment.Where(y => y.DeliveryManagerId == x.CompanyId).Count() < 5).FirstOrDefault();

            var quoteModel = new QuoteModel
            {
                Customer = customerName,
                RequiredSkills = requiredSkills,
                AccountManager = underloadedAccountManager.FirstName + " " + underloadedAccountManager.LastName,
                DeliveryManager = underloadedDeliveryManager.FirstName + " " + underloadedDeliveryManager.LastName,
                Team = await AssembleTeamBasedOnSkills(requiredSkills, employees, _skillsService)
            };

            return quoteModel;
        }

        /// <summary>
        /// Assembles a development team from the available experts in the department
        /// </summary>
        /// <param name="requiredSkills">the skills mentioned by the customer</param>
        /// <param name="employees">the department employees</param>
        /// <param name="skillsService">the Skills retrieval service</param>
        /// <returns>a sting listing the team members and their details</returns>
        private async Task<string> AssembleTeamBasedOnSkills(string requiredSkills, IEnumerable<EmployeeModel> employees, ISkillsService skillsService)
        {
            // The final team 
            List<EmployeeModel> team = new List<EmployeeModel>();

            // The skills of the employees in the department
            List<SkillSetModel> employeeSkillSets = new List<SkillSetModel>();

            // An array of the required skills by the customer
            string[] skillsArray = requiredSkills.Split(';');

            // Retrieving the skill sets of the company's employees
            foreach(var employee in employees)
            {
                var employeeSkills = await skillsService.GetSkillSet(employee.FirstName, employee.LastName, employee.CompanyId);
                employeeSkillSets.Add(employeeSkills);
            }

            // Assembling the team
            foreach(var skill in skillsArray)
            {
                var employeeSkillSet = employeeSkillSets.Where(x => x.Items.Where(y => y.Skill == skill).Count() > 0).FirstOrDefault();
                
                if (employeeSkillSet != null)
                {
                    // Checking if there is a team member with the given skillSet already added in the team - if not, we add the team member
                    if (team.Where(x=>x.CompanyId == employeeSkillSet.CompanyId).Count() == 0)
                    {
                        team.Add(employees.Where(x => x.CompanyId == employeeSkillSet.CompanyId).FirstOrDefault());
                    }
                }
            }

            return GetTeamDetails(team);
        }

        private string GetTeamDetails(List<EmployeeModel> team)
        {
            StringBuilder teamBuilder = new StringBuilder();

            // Building the team details as a string
            foreach (var teamMember in team)
            {
                teamBuilder.Append(teamMember.FirstName);
                teamBuilder.Append(" ");
                teamBuilder.Append(teamMember.LastName);
                teamBuilder.Append(" - ");
                teamBuilder.Append(teamMember.Role);
                teamBuilder.Append(", Email (");
                teamBuilder.Append(teamMember.Email);
                teamBuilder.Append(");");
            }

            return teamBuilder.ToString();
        }
    }
}
