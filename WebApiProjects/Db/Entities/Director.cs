using System.ComponentModel.DataAnnotations;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Db.Entities
{
    public class Director
    {
        [Key]
        public Guid ProducerId { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public List<MovieEntity> Movies { get; set; }

        public Director()
        {
            Movies = new List<MovieEntity>();
        }


    }
}
