using Dapper;
using Erp.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration _configuration;

        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Project> GetProject(int id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var project = await connection.QueryFirstOrDefaultAsync<Project>
                ("SELECT * FROM Project WHERE Id = @Id", new { Id = id });

            if (project == null)
                return new Project
                { Customer = "No customer", StartDate = DateTime.Today, EndDate = DateTime.Today, Budget = 0, Status = 0, Team = "Not specified" };

            return project;
        }

        public async Task<bool> CreateProject(Project project)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Project (Customer, StartDate, EndDate, Budget, Status, Team) VALUES (@Customer, @StartDate, @EndDate, @Budget, @Status, @Team)",
                        new { Customer = project.Customer, StartDate = project.StartDate, EndDate = project.EndDate, Budget = project.Budget, Status = project.Status, Team = project.Team });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateProject(Project project)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE Project SET Customer=@Customer, StartDate=@StartDate, EndDate=@EndDate, Budget=@Budget, Status=@Status, Team=@Team WHERE Id=@Id",
                    new { Customer = project.Customer, StartDate = project.StartDate, EndDate = project.EndDate, Budget = project.Budget, Status = project.Status, Team = project.Team });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteProject(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Project WHERE Id = @Id",
                new { Id = id });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
