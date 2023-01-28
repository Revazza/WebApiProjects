using MoviesDatabase.Api.Db.Entities;
using MoviesDatabase.Api.Models.Requests;
using System.Reflection.PortableExecutable;
using WebApiProjects.Db;

namespace MoviesDatabase.Api.Services
{
    public interface IDirectorRepository
    {
        Task AddDirectorAsync(AddDirectorRequest request);
        Task SaveChangesAsync();
    }
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MoviesDbContext _context;

        public DirectorRepository(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task AddDirectorAsync(AddDirectorRequest request)
        {
            var newDirector = new DirectorEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
            };

            await _context.Directors.AddAsync(newDirector);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
