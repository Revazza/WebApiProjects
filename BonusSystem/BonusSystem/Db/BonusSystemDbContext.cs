using BonusSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Db
{
    public class BonusSystemDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<BonusEntity> Bonuses { get; set; }


        public BonusSystemDbContext(DbContextOptions<BonusSystemDbContext> options) : base(options)
        {


        }


    }
}
