using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Models.Dto
{
    public class MovieDto
    {
        public Guid MovieId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public MovieStatus Status { get; set; } = MovieStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
