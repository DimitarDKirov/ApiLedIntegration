using Employees.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employees.API.Data
{
    public class EmployeesContextSeed
    {
        public static void SeedData(IMongoCollection<Employee> employeeCollection)
        {
            bool existEmployee  = employeeCollection.Find(p => true).Any();
            if (!existEmployee)
            {
                employeeCollection.InsertManyAsync(GetPreconfiguredEmployees());
            }
        }

        private static IEnumerable<Employee> GetPreconfiguredEmployees()
        {
            return new List<Employee>()
            {
				new Employee()
				{
					FirstName = "Ivan",
					LastName = "Ivanov",
					Email = "ivan.ivanov@company.com",
					Role = "Senior .NET Developer",
					Department = ".NET",
					Phone = "0898786858",
					CompanyId = 1
				},
				new Employee()
				{
					FirstName = "Maria",
					LastName = "Petrova",
					Email = "maria.petrova@company.com",
					Role = "Senior Java Developer",
					Department = "Java",
					Phone = "0898486848",
					CompanyId = 2
				},
				new Employee()
				{
					FirstName = "Asen",
					LastName = "Ivanov",
					Email = "asen.ivanov@company.com",
					Role = "Project Manager",
					Department = "Project Management",
					Phone = "0898433848",
					CompanyId = 3
				},
				new Employee()
				{
					FirstName = "Kalina",
					LastName = "Kalinova",
					Email = "kalina.kalinova@company.com",
					Role = "Project Manager",
					Department = "Project Management",
					Phone = "0898433828",
					CompanyId = 4
				},
				new Employee()
				{
					FirstName = "Vasil",
					LastName = "Vasilev",
					Email = "vasil.vasilev@company.com",
					Role = ".NET Developer",
					Department = ".NET",
					Phone = "0891473828",
					CompanyId = 5
				},
				new Employee()
				{
					FirstName = "Petar",
					LastName = "Vasilev",
					Email = "petar.vasilev@company.com",
					Role = "Java Developer",
					Department = "Java",
					Phone = "0891473848",
					CompanyId = 6
				},
				new Employee()
				{
					FirstName = "Asen",
					LastName = "Vasilev",
					Email = "asen.vasilev@company.com",
					Role = "Project Manager",
					Department = "Project Management",
					Phone = "0891473848",
					CompanyId = 7
				},
				new Employee()
				{
					FirstName = "Katia",
					LastName = "Petrova",
					Email = "katia.petrova@company.com",
					Role = "PHP Developer",
					Department = "PHP",
					Phone = "0898483348",
					CompanyId = 8
				},
				new Employee()
				{
					FirstName = "Elena",
					LastName = "Petrova",
					Email = "elena.petrova@company.com",
					Role = "Senior PHP Developer",
					Department = "PHP",
					Phone = "0898487748",
					CompanyId = 9
				},
				new Employee()
				{
					FirstName = "Milena",
					LastName = "Petrova",
					Email = "milena.petrova@company.com",
					Role = "Project Manager",
					Department = "Project Management",
					Phone = "0898482148",
					CompanyId = 10
				},
				new Employee()
				{
					FirstName = "Silvia",
					LastName = "Petrova",
					Email = "silvia.petrova@company.com",
					Role = "Project Coordinator",
					Department = "Project Management",
					Phone = "0893382148",
					CompanyId = 11
				},
				new Employee()
				{
					FirstName = "Kiril",
					LastName = "Kirilov",
					Email = "kiril.kirilov@company.com",
					Role = "Junior .NET Developer",
					Department = ".NET",
					Phone = "0898487722",
					CompanyId = 12
				},
				new Employee()
				{
					FirstName = "Nikola",
					LastName = "Kirilov",
					Email = "nikola.kirilov@company.com",
					Role = "Junior Java Developer",
					Department = "Java",
					Phone = "0898487711",
					CompanyId = 13
				},
				new Employee()
				{
					FirstName = "Asen",
					LastName = "Kirilov",
					Email = "asen.kirilov@company.com",
					Role = "Junior PHP Developer",
					Department = "PHP",
					Phone = "0898486511",
					CompanyId = 14
				},
				new Employee()
				{
					FirstName = "Tania",
					LastName = "Petrova",
					Email = "tania.petrova@company.com",
					Role = "UX Designer",
					Department = "UX",
					Phone = "0893321211",
					CompanyId = 15
				},
				new Employee()
				{
					FirstName = "Dimitar",
					LastName = "Petrov",
					Email = "dimitar.petrov@company.com",
					Role = "Senior UX Designer",
					Department = "UX",
					Phone = "0893321299",
					CompanyId = 16
				},
				new Employee()
				{
					FirstName = "Albena",
					LastName = "Petrova",
					Email = "albena.petrova@company.com",
					Role = "Senior UX Designer",
					Department = "UX",
					Phone = "0893321288",
					CompanyId = 17
				},
				new Employee()
				{
					FirstName = "Trifon",
					LastName = "Trifonov",
					Email = "trifon.trifonov@company.com",
					Role = "Delivery Manager",
					Department = ".NET",
					Phone = "0897721288",
					CompanyId = 18
				},
				new Employee()
				{
					FirstName = "Asen",
					LastName = "Asenov",
					Email = "asen.asenov@company.com",
					Role = "Delivery Manager",
					Department = "Java",
					Phone = "0897721288",
					CompanyId = 19
				},
				new Employee()
				{
					FirstName = "Angel",
					LastName = "Angelov",
					Email = "angel.angelov@company.com",
					Role = "Delivery Manager",
					Department = "PHP",
					Phone = "0897741288",
					CompanyId = 20
				},
				new Employee()
				{
					FirstName = "Petar",
					LastName = "Angelov",
					Email = "petar.angelov@company.com",
					Role = "Account Manager",
					Department = "Sales",
					Phone = "0897751288",
					CompanyId = 21
				},
				new Employee()
				{
					FirstName = "Marin",
					LastName = "Marinov",
					Email = "marin.marinov@company.com",
					Role = "Account Manager",
					Department = "Sales",
					Phone = "0897731288",
					CompanyId = 22
				},
				new Employee()
				{
					FirstName = "Ivanka",
					LastName = "Ivanova",
					Email = "marin.marinov@company.com",
					Role = "Delivery Manager",
					Department = "UX",
					Phone = "0897751276",
					CompanyId = 23
				}
			};
        }
    }
}
