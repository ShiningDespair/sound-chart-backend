using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ChinookContext _context;

        public ArtistsController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/Artists unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.Distinct().ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        //Get: api/Artists/Cloud
        [HttpGet("Cloud")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsCloud()
        {
            var query = from a in _context.Artists
                        join al in _context.Albums on a.ArtistId equals al.ArtistId
                        join t in _context.Tracks on al.AlbumId equals t.AlbumId
                        join il in _context.InvoiceLines on t.TrackId equals il.TrackId
                        group new { a, il } by a.Name into g
                        select new ArtistCloudDto
                        {
                            Name = g.Key,
                            Sale = g.Sum(x => x.il.Quantity * x.il.UnitPrice)
                        };
            var result = await query
                .Where(x => x.Sale > 1)
                .ToListAsync();

            return Ok(result);
        }

    }
}
