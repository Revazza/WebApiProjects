using BonusSystem.Db.Entities;

namespace BonusSystem.Models
{
    public class AddEmployeeRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public double Salary { get; set; }
        public Guid? RecommendatorId { get; set; }

    }
}
