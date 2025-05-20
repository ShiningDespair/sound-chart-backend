using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaTypesController : ControllerBase
    {
        private readonly ChinookContext _context;

        public MediaTypesController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/MediaTypes unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaType>>> GetMediaTypes()
        {
            return await _context.MediaTypes.Distinct().ToListAsync();
        }

        // GET: api/MediaTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaType>> GetMediaType(int id)
        {
            var mediaType = await _context.MediaTypes.FindAsync(id);

            if (mediaType == null)
            {
                return NotFound();
            }

            return mediaType;
        }
    }
}
