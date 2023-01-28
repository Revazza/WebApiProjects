using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Api.Db.Entities;
using MoviesDatabase.Api.Models.Dto;
using MoviesDatabase.Api.Models.Requests;
using WebApiProjects.Db;
using WebApiProjects.Db.Entities;

namespace MoviesDatabase.Api.Services
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(AddMovieRequest request);
        Task<List<MovieEntity>> GetAllMoviesAsync();
        Task<MovieEntity?> GetMovieByIdAsync(Guid id);
        Task<List<MovieDto>> SearchMoviesAsync(SearchMovieRequest request);
        Task<MovieEntity> UpdateMovieAsync(UpdateMovieRequest request);
        Task DeleteMovieAsync(Guid movieId);
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

            directors.ForEach(d => d.Movies.Add(newMovie));

            await _context.Movies.AddAsync(newMovie);

        }

        public async Task<List<MovieEntity>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<MovieEntity?> GetMovieByIdAsync(Guid id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);

            return movie != null ? movie : null;
        }

        public async Task<List<MovieDto>> SearchMoviesAsync(SearchMovieRequest request)
        {
            int moviesToSkipAmount = request.PageSize * request.PageIndex;
            var movies = _context.Movies
                .Skip(moviesToSkipAmount)
                .Take(request.PageSize)
                .Include(m => m.Directors).AsQueryable();

            if (!string.IsNullOrEmpty(request.MovieName))
            {
                movies = movies.Where(m => m.Name!.Contains(request.MovieName));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                movies = movies.Where(m => m.Description!.Contains(request.Description));
            }

            if (!string.IsNullOrEmpty(request.DirectorName))
            {
                movies = movies.Where(m => m.Directors.Any(d => d.FirstName!.Contains(request.DirectorName)));
            }
            if (request.RealiseDate != DateTime.MinValue)
            {
                movies = movies.Where(m => m.ReleaseDate >= request.RealiseDate);
            }
            if (request.Ordered)
            {
                movies = movies.OrderBy(m => m.ReleaseDate);
            }

            return await movies
                .Select(m => new MovieDto()
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    Description = m.Description,
                    Status = m.Status,
                    ReleaseDate = m.ReleaseDate,
                    CreatedAt = m.CreatedAt,
                }
                ).ToListAsync();
        }

        public async Task<MovieEntity> UpdateMovieAsync(UpdateMovieRequest request)
        {
            var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == request.MovieId);

            if (movieToUpdate == null)
            {
                throw new ArgumentException("Can't identify movie");
            }

            movieToUpdate.Name = request.Name;
            movieToUpdate.Description = request.Description;
            movieToUpdate.ReleaseDate = request.ReleaseDate;

            _context.Movies.Update(movieToUpdate);

            return movieToUpdate;
        }

        public async Task DeleteMovieAsync(Guid movieId)
        {
            var movie = await _context.Movies
                             .Include(m => m.Directors)
                             .FirstOrDefaultAsync(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Can't identify movie");
            }
            movie.Status = MovieStatus.Deleted;

            _context.Movies.Update(movie);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
