using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Api.Db.Entities;
using MoviesDatabase.Api.Models.Requests;
using WebApiProjects.Db;

namespace MoviesDatabase.Api.Services
{
    public interface IDirectorRepository
    {
        Task AddDirectorAsync(AddDirectorRequest request);
        Task<List<DirectorEntity>> GetAllDirectors();
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

        public async Task<List<DirectorEntity>> GetAllDirectors()
        {
            return await _context.Directors.Include(d => d.Movies).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
