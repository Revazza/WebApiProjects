using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Api.Db.Entities;
using WebApiProjects.Db.Entities;

namespace WebApiProjects.Db
{
    public class MoviesDbContext : DbContext
    {

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MovieEntity>(e =>
            {
                e.Property(e => e.Name)
                 .HasMaxLength(200);
                e.Property(e => e.Description)
                 .HasMaxLength(2000);
                e.Property(e => e.ReleaseDate)
                 .IsRequired();
                e.Property(e => e.Status)
                 .IsRequired();
                e.Property(e => e.CreatedAt)
                 .IsRequired();
            });

            builder.Entity<Director>()
                    .HasMany(d => d.Movies)
                    .WithMany(m => m.Directors)
                    .UsingEntity(j => j.ToTable("DirectorMovies"));

        }
    }
}
