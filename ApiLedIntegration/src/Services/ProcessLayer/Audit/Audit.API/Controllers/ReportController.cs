using Audit.API.Models;
using Audit.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Audit.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IOpportunityService _opportunityService;
        private readonly IProjectService _projectService;

        public ReportController(IOpportunityService opportunityService, IProjectService projectService)
        {
            _opportunityService = opportunityService ?? throw new ArgumentNullException(nameof(opportunityService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        [HttpGet(Name = "GetReport")]
        [ProducesResponseType(typeof(ReportModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportModel>> GetReport()
        {
            var opportunities = await _opportunityService.GetOpportunities();
            var projects = await _projectService.GetProjects();

            int startedProjectsCount = projects.Count();
            int finishedProjectsCount = projects.Where(x => x.Status == 3).Count();
            int failedProjectsCount = projects.Where(x => x.Status == 5).Count();

            var reportModel = new ReportModel
            {
                StartedProjects = startedProjectsCount,
                FinishedProjects = finishedProjectsCount,
                FailedProjects = failedProjectsCount,
                StartToFailRatio = (double)startedProjectsCount / (double)failedProjectsCount,
                StartToFinishRatio = (double)startedProjectsCount / (double)finishedProjectsCount,
                OpportunityToProjectRatio = (double)opportunities.Count() / (double)projects.Count(),
                AverageDaysToFinishNet = GetAverageDaysToFinishPerDepartment(projects.Where(x => x.Department.Equals(".NET") && x.Status == 3)),
                AverageDaysToFinishJava = GetAverageDaysToFinishPerDepartment(projects.Where(x => x.Department.Equals("Java") && x.Status == 3)),
                AverageDaysToFinishPhp = GetAverageDaysToFinishPerDepartment(projects.Where(x => x.Department.Equals("PHP") && x.Status == 3)),
                AverageDaysToFinishUx = GetAverageDaysToFinishPerDepartment(projects.Where(x => x.Department.Equals("UX") && x.Status == 3)),
                AverageBudgetForNet = GetAverageBudgetToFinishForDepartment(projects.Where(x => x.Department.Equals(".NET") && x.Status == 3)),
                AverageBudgetForJava = GetAverageBudgetToFinishForDepartment(projects.Where(x => x.Department.Equals("Java") && x.Status == 3)),
                AverageBudgetForPhp = GetAverageBudgetToFinishForDepartment(projects.Where(x => x.Department.Equals("PHP") && x.Status == 3)),
                AverageBudgetForUx = GetAverageBudgetToFinishForDepartment(projects.Where(x => x.Department.Equals("UX") && x.Status == 3)),
            };

            return Ok(reportModel);
        }

        private int GetAverageDaysToFinishPerDepartment(IEnumerable<ProjectModel> projects)
        {
            int projectCount = projects.Count();
            int totalWorkDaysForFinishedProjects = 0;

            foreach(var project in projects)
            {
                totalWorkDaysForFinishedProjects += Convert.ToInt32((project.EndDate - project.StartDate).TotalDays);
            }

            if (totalWorkDaysForFinishedProjects == 0) return 0;
            if (projectCount == 0) return 0;

            return totalWorkDaysForFinishedProjects / projectCount;
        }

        private double GetAverageBudgetToFinishForDepartment(IEnumerable<ProjectModel> projects)
        {
            int projectCount = projects.Count(x => x.Status == 3);
            double totalBudgetForFinishedProjects = 0.0;

            foreach (var project in projects)
            {
                totalBudgetForFinishedProjects += project.Budget;
            }

            if (totalBudgetForFinishedProjects == 0) return 0;
            if (projectCount == 0) return 0;

            return Math.Round(totalBudgetForFinishedProjects / projectCount, 2);
        }
    }
}
