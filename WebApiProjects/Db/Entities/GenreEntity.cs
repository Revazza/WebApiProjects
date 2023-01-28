using System.ComponentModel.DataAnnotations;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Db.Entities
{
    public class GenreEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public List<MovieEntity> Movies { get; set; }

        public GenreEntity()
        {
            Movies = new List<MovieEntity>();
        }

    }
}
