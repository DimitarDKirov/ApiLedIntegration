﻿using Employees.API.Entities;
using Employees.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Employees.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeRepository repository, ILogger<EmployeesController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _repository.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> GetEmployeeById(string id)
        {
            var employee = await _repository.GetEmployee(id);
            if(employee == null)
            {
                _logger.LogError($"Employee with id: {id}, not found.");
                return NotFound();
            }
            return Ok(employee);
        }

        [Route("[action]/{companyId}", Name = "GetEmployeeByCompanyId")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> GetEmployeeByCompanyId(int companyId)
        {
            var employee = await _repository.GetEmployeeByCompanyId(companyId);
            if (employee == null)
            {
                _logger.LogError($"Employee with company Id: {companyId}, not found.");
                return NotFound();
            }
            return Ok(employee);
        }

        [Route("[action]/{department}", Name = "GetEmployeesByDepartment")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> GetEmployeesByDepartment(string department)
        {
            var employees = await _repository.GetEmployeesByDepartment(department);
            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            await _repository.CreateEmployee(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            return Ok(await _repository.UpdateEmployee(employee));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteEmployee")]
        [ProducesResponseType(typeof(Employee), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEmployeeById(string id)
        {
            return Ok(await _repository.DeleteEmployee(id));
        }
    }
}
