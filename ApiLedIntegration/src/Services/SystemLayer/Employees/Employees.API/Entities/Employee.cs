using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Employees.API.Entities
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public int CompanyId { get; set; }
    }
}
