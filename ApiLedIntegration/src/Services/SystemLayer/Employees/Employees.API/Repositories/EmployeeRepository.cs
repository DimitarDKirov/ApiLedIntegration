using Employees.API.Data;
using Employees.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEmployeesContext _context;

        public EmployeeRepository(IEmployeesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context
                            .Employees
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _context
                            .Employees
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            return await _context
                            .Employees
                            .Find(p => p.Department == department)
                            .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByCompanyId(int companyId)
        {
            return await _context
                            .Employees
                            .Find(p => p.CompanyId == companyId)
                            .FirstOrDefaultAsync();
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _context.Employees.InsertOneAsync(employee);
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var updateResult = await _context
                                        .Employees
                                        .ReplaceOneAsync(filter: g => g.Id == employee.Id, replacement: employee);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteEmployee(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Employees
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                    && deleteResult.DeletedCount > 0;
        }
    }
}
