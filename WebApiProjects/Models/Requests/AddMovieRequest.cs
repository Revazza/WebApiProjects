using MoviesDatabase.Api.Db.Entities;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Models.Requests
{
    public class AddMovieRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Guid>? DirectorIds { get; set; } = new List<Guid>();

    }
}
