using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Models.Requests
{
    public class AddDirectorRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }

    }
}
