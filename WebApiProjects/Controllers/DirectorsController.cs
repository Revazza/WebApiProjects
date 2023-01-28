using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Api.Models.Requests;
using MoviesDatabase.Api.Services;

namespace MoviesDatabase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorsController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }


        [HttpPost("add-director")]
        public async Task<IActionResult> AddDirector(AddDirectorRequest request)
        {
            
            await _directorRepository.AddDirectorAsync(request);

            await _directorRepository.SaveChangesAsync();

            return Ok();
        }


    }
}
