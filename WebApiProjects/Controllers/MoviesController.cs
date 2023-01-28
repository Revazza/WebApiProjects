using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MoviesDatabase.Api.Models.Requests;
using MoviesDatabase.Api.Services;

namespace MoviesDatabase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _moviesService;

        public MoviesController(IMovieRepository moviesService)
        {
            _moviesService = moviesService;
        }


        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovie(AddMovieRequest request)
        {
            if (!request.DirectorIds!.Any())
            {
                return BadRequest("No directors specified");
            }
            await _moviesService.AddMovieAsync(request);

            await _moviesService.SaveChangesAsync();

            return Ok("");
        }

        [HttpGet("get-movies-by-id/{movieId}")]
        public async Task<IActionResult> GetMovieById(Guid movieId)
        {
            var movie = await _moviesService.GetMovieByIdAsync(movieId);

            return movie != null ? Ok(movie) : BadRequest("Movie doesn't exist");
        }

        [HttpPost("search-movies")]
        public async Task<IActionResult> SearchMovies(SearchMovieRequest request)
        {
            var filteredMovies = await _moviesService.SearchMovies(request);

            return Ok(filteredMovies);
        }

        [HttpPost("update-movie")]
        public async Task<IActionResult> UpdateMovie(UpdateMovieRequest request)
        {
            try
            {
                var updatedMovie = await _moviesService.UpdateMovieAsync(request);

                await _moviesService.SaveChangesAsync();

                return Ok(updatedMovie);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            await _moviesService.DeleteMovieAsync(movieId);

            await _moviesService.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-all-movies")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _moviesService.GetAllMoviesAsync();
            return Ok(movies);
        }


    }
}
