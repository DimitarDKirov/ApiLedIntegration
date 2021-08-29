using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestForQuote.API.Models
{
    public class ReportModel
    {
        // Project specific statistics
        public int StartedProjects { get; set; }

        public int FinishedProjects { get; set; }

        public int FailedProjects { get; set; }

        public double StartToFailRatio { get; set; }

        public double StartToFinishRatio { get; set; }

        public double OpportunityToProjectRatio { get; set; }

        // Department specific statistics

        public int AverageDaysToFinishNet { get; set; }

        public int AverageDaysToFinishJava { get; set; }

        public int AverageDaysToFinishPhp { get; set; }

        public int AverageDaysToFinishUx { get; set; }

        public double AverageBudgetForNet { get; set; }

        public double AverageBudgetForJava { get; set; }

        public double AverageBudgetForPhp { get; set; }

        public double AverageBudgetForUx { get; set; }
    }
}
