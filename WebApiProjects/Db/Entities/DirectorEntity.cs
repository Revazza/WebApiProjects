using System.ComponentModel.DataAnnotations;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Db.Entities
{
    public class DirectorEntity
    {
        [Key]
        public Guid DirectorId { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public List<MovieEntity> Movies { get; set; }

        public DirectorEntity()
        {
            Movies = new List<MovieEntity>();
        }


    }
}
