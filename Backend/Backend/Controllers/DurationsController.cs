using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for managing track durations in the Chinook database.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DurationsController : ControllerBase
    {
        private readonly ChinookContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="DurationsController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public DurationsController(ChinookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the minimum and maximum track durations in seconds from the database.
        /// </summary>
        /// <returns>Two item list of <see cref="DurationDto"/> objects. </returns>
        [HttpGet]
        public async Task<IActionResult> GetDurationRange()
        {
            var min = await _context.Tracks.MinAsync(t => t.Milliseconds)/1000;
            var max = await _context.Tracks.MaxAsync(t => t.Milliseconds)/1000;
            var durationDto = new DurationDto(min, max);

            return Ok(durationDto);
        }
    }

}
