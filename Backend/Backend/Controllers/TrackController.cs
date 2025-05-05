using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/tracks")]
    public class TrackController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTracks()
        {
            // Simulate fetching data from a database or service
            var tracks = new[]
            {
                new { Id = 1, Name = "Track 1" },
                new { Id = 2, Name = "Track 2" },
                new { Id = 3, Name = "Track 3" }
            };
            return Ok(tracks);
        }

        [HttpGet]
        public IActionResult GetTracksMap()
        {
            var body = JsonSerializer.Serialize(new
            {
                    
            });

            return Ok(body);
        }
    }
}
