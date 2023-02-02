namespace BonusSystem.Db.Entities
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public double Salary { get; set; }
        public Guid RecommendatorId { get; set; } = Guid.Empty;
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;





    }
}
