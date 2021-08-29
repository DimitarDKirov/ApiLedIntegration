using RequestForQuote.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestForQuote.API.Services
{
    public interface IAuditService
    {
        Task<ReportModel> GetReport();
    }
}
