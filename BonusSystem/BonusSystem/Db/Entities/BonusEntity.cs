using System.ComponentModel.DataAnnotations;

namespace BonusSystem.Db.Entities
{
    public class BonusEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        public Guid RecommendatorId { get; set; } = Guid.Empty;
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;



    }
}
