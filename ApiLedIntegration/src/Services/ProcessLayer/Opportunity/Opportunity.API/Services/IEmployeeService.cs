using Opportunity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Opportunity.API.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployee(string id);
        Task<EmployeeModel> GetEmployeeByCompanyId(int companyId);
        Task<IEnumerable<EmployeeModel>> GetEmployeesByDepartment(string department);
    }
}
