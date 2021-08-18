using Employees.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.API.Data
{
    public interface IEmployeesContext
    {
        IMongoCollection<Employee> Employees { get; }
    }
}
