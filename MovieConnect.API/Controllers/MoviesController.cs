using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieConnect.Application.DTOs;
using MovieConnect.Application.Queries;

namespace MovieConnect.API.Controllers
{  
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get movies by name
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(MovieResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
            {
                return BadRequest();
            }

            var result = await mediator.Send(new GetMoviesByName(movieName));
            return Ok(result);
        }
    }
}