using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DurationsController : ControllerBase
    {
        private readonly ChinookContext _context;

        public DurationsController(ChinookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDurationRange()
        {
            var min = await _context.Tracks.MinAsync(t => t.Milliseconds);
            var max = await _context.Tracks.MaxAsync(t => t.Milliseconds);
            return Ok(new { min, max });
        }
    }

}
