using Employees.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.API.Data
{
    public class EmployeesContext : IEmployeesContext
    {
        public EmployeesContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Employees = database.GetCollection<Employee>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            EmployeesContextSeed.SeedData(Employees);
        }

        public IMongoCollection<Employee> Employees { get; }
    }
}
