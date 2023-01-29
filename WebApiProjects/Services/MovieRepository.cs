using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MoviesDatabase.Api.Db.Entities;
using MoviesDatabase.Api.Models.Dto;
using MoviesDatabase.Api.Models.Requests;
using System.IO;
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
        Task<List<GenreEntity>> GetAllGenresAsync();
        Task DeleteMovieAsync(Guid movieId);
        Task<List<MovieDto>> SearchMovieByGenresAsync(string genre);
        Task SaveChangesAsync();

    }


    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesDbContext _context;

        private async Task AddMovieToDirectorsAsync(List<Guid> directorIds, List<DirectorEntity> directors)
        {
            foreach (var directorId in directorIds)
            {
                var director = await _context.Directors
                    .FirstOrDefaultAsync(d => d.Id == directorId);

                if (director == null)
                {
                    throw new ArgumentException("Can't identify director");
                }
                directors.Add(director);
            }
        }
        private async Task AddMovieToGenresAsync(List<Guid> directorIds, List<GenreEntity> genres)
        {

            foreach (var genreId in directorIds)
            {
                var genre = await _context.Genres
                    .FirstOrDefaultAsync(d => d.Id == genreId);

                if (genre == null)
                {
                    throw new ArgumentException("Can't identify director");
                }
                genres.Add(genre);
            }
        }

        public MovieRepository(MoviesDbContext context)
        {
            _context = context;
        }
        public async Task AddMovieAsync(AddMovieRequest request)
        {
            var directors = new List<DirectorEntity>();
            var genres = new List<GenreEntity>();

            await AddMovieToDirectorsAsync(request.DirectorIds!, directors!);
            await AddMovieToGenresAsync(request.GenreIds!, genres);

            var newMovie = new MovieEntity()
            {
                Name = request.Name,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
            };

            await _context.Movies.AddAsync(newMovie);

            directors.ForEach(d => d.Movies.Add(newMovie));
            genres.ForEach(g => g.Movies.Add(newMovie));

        }

        // აქაც და მგონი სხვა ადგილებში სტატუსი წაშლილის რომ არ ქონდეს მაგასაც უნდა ამოწმებდე, 
        // თუ ამოწმებ და ვერ ვნახე, მეპატიება. შენსავით მაგარი არ ვარ. გითში რომ ვივარჯიშო უფრო იმიტომ ვწერ ამას.
        public async Task<List<MovieEntity>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Genres).ToListAsync();
        }

        public async Task<MovieEntity?> GetMovieByIdAsync(Guid id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

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
                    MovieId = m.Id,
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
            var movieToUpdate = await _context.Movies.FirstOrDefaultAsync(m => m.Id == request.MovieId);

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
                             .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                throw new ArgumentException("Can't identify movie");
            }
            movie.Status = MovieStatus.Deleted;

            _context.Movies.Update(movie);
        }
        public async Task<List<GenreEntity>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<List<MovieDto>> SearchMovieByGenresAsync(string genres)
        {
            var movies = _context.Movies.Include(m => m.Genres);
            var filteredMoviesByGenre = movies.Where(m =>
                m.Genres.Any(d =>
                d.Name!.Contains(genres)));


            return await filteredMoviesByGenre
                .Select(m => new MovieDto()
                {
                    MovieId = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Status = m.Status,
                    ReleaseDate = m.ReleaseDate,
                    CreatedAt = m.CreatedAt,
                }
                ).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
