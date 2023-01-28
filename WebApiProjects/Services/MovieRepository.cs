using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Api.Db.Entities;
using MoviesDatabase.Api.Models.Requests;
using WebApiProjects.Db;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Services
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(AddMovieRequest request);
        Task SaveChangesAsync();
    }


    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _context;

        public MovieRepository(MoviesDbContext context)
        {
            _context = context;
        }
        public async Task AddMovieAsync(AddMovieRequest request)
        {
            var directors = new List<DirectorEntity>();

            foreach (var directorId in request.DirectorIds)
            {
                var director = await _context.Directors
                    .FirstOrDefaultAsync(d => d.DirectorId == directorId);

                if (director == null)
                {
                    throw new ArgumentException("Can't identify director");
                }
                directors.Add(director);
            }

            var newMovie = new MovieEntity()
            {
                Name = request.Name,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
            };

            await _context.Movies.AddAsync(newMovie);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
