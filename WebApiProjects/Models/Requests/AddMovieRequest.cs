using Azure.Core;
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


        public void Validate()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length > 200)
            {
                throw new ArgumentNullException("Name is not specified");
            }
            if (string.IsNullOrEmpty(Description) || Description.Length > 2000)
            {
                throw new ArgumentNullException("Description is not specified");
            }
            if (DateTime.Now.Year > ReleaseDate.Year)
            {
                throw new ArgumentNullException("Name is not specified");
            }
            if (DirectorIds!.Any())
            {
                throw new ArgumentException("No directors specified");
            }
        }
    }
}
