using MoviesDatabase.Api.Db.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApiProjects.Db.Entities
{

    public enum MovieStatus
    {
        Active,
        Deleted
    }


    public class MovieEntity
    {
        [Key]
        public Guid MovieId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public MovieStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Director> Directors { get; set; }

        public MovieEntity()
        {
            Directors = new List<Director>();
        }

    }
}
