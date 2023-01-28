using MoviesDatabase.Api.Db.Entities;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Models.Requests
{
    public class UpdateMovieRequest
    {
        public Guid MovieId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
