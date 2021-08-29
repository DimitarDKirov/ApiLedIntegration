using Crm.API.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.API.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly IConfiguration _configuration;

        public OpportunityRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Opportunity> GetOpportunity(int id)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var opportunity = await connection.QueryFirstOrDefaultAsync<Opportunity>
                ("SELECT * FROM Opportunity WHERE Id = @Id", new { Id = id });

            if (opportunity == null)
                return new Opportunity
                { Customer = "No customer", RequiredSkills = "Not specified", AccountManagerId = 0, DeliveryManagerId = 0 };

            return opportunity;
        }

        public async Task<bool> CreateOpportunity(Opportunity opportunity)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Opportunity (Customer, RequiredSkills, AccountManagerId, DeliveryManagerId, Department) VALUES (@Customer, @RequiredSkills, @AccountManagerId, @DeliveryManagerId)",
                        new { Customer = opportunity.Customer, RequiredSkills = opportunity.RequiredSkills, AccountManagerId = opportunity.AccountManagerId, DeliveryManagerId = opportunity.DeliveryManagerId, Department = opportunity.Department });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateOpportunity(Opportunity opportunity)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync
                ("UPDATE Opportunity SET Customer=@Customer, RequiredSkills=@RequiredSkills, AccountManagerId=@AccountManagerId, DeliveryManagerId=@DeliveryManagerId, Department = @Department WHERE Id=@Id",
                    new { Customer = opportunity.Customer, RequiredSkills = opportunity.RequiredSkills, AccountManagerId = opportunity.AccountManagerId, DeliveryManagerId = opportunity.DeliveryManagerId, Department = opportunity.Department });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteOpportunity(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("DELETE FROM Opportunity WHERE Id = @Id",
                new { Id = id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<IEnumerable<Opportunity>> GetOpportunities()
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var opportunities = await connection.QueryAsync<Opportunity>
                ("SELECT * FROM Opportunity");

            if (opportunities == null)
                return new List<Opportunity>() { };

            return opportunities;
        }
    }
}
