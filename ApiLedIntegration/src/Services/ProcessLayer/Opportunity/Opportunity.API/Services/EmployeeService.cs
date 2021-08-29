using Opportunity.API.Extensions;
using Opportunity.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Opportunity.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _client;

        public EmployeeService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<EmployeeModel> GetEmployee(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Employees/{id}");
            return await response.ReadContentAs<EmployeeModel>();
        }

        public async Task<EmployeeModel> GetEmployeeByCompanyId(int companyId)
        {
            var response = await _client.GetAsync($"/api/v1/Employees/GetEmployeeByCompanyId/{companyId}");
            return await response.ReadContentAs<EmployeeModel>();
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            var response = await _client.GetAsync($"/api/v1/Employees");
            return await response.ReadContentAs<List<EmployeeModel>>();
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesByDepartment(string department)
        {
            var response = await _client.GetAsync($"/api/v1/Employees/GetEmployeesByDepartment/{department}");
            return await response.ReadContentAs<List<EmployeeModel>>();
        }
    }
}
