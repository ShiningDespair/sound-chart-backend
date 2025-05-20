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
    public class TracksController : ControllerBase
    {
        private readonly ChinookContext _context;

        public TracksController(ChinookContext context)
        {
            _context = context;
        }


        [HttpGet("worldMap")]
        public async Task<ActionResult<IEnumerable<object>>> GetTracksMap(
            [FromQuery] string? genre,
            [FromQuery] int? minDuration,
            [FromQuery] int? maxDuration,
            [FromQuery] string? artist,
            [FromQuery] string? album,
            [FromQuery] string? mediaType)
        {
            var query = from il in _context.InvoiceLines
                        join t in _context.Tracks on il.TrackId equals t.TrackId
                        join g in _context.Genres on t.GenreId equals g.GenreId
                        join m in _context.MediaTypes on t.MediaTypeId equals m.MediaTypeId
                        join i in _context.Invoices on il.InvoiceId equals i.InvoiceId
                        join c in _context.Customers on i.CustomerId equals c.CustomerId
                        join co in _context.Countries on c.CountryId equals co.CountryId
                        join a in _context.Albums on t.AlbumId equals a.AlbumId
                        join ar in _context.Artists on a.ArtistId equals ar.ArtistId
                        select new { il, t, g, m, a, ar, c, co };

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(x => x.g.Name == genre);

            if (!string.IsNullOrEmpty(mediaType))
                query = query.Where(x => x.m.Name == mediaType);

            if (minDuration.HasValue)
                query = query.Where(x => x.t.Milliseconds >= minDuration.Value);

            if (maxDuration.HasValue)
                query = query.Where(x => x.t.Milliseconds <= maxDuration.Value);

            if (!string.IsNullOrEmpty(artist))
                query = query.Where(x => x.ar.Name.Contains(artist));

            if (!string.IsNullOrEmpty(album))
                query = query.Where(x => x.a.Title.Contains(album));

            // Group by CountryId, but return the associated CountryIsoCode
            var grouped = await query
                .GroupBy(x => x.c.CountryId)  // Group by CountryId
                .Select(g => new
                {
                    CountryIsoCode = g.FirstOrDefault().co.CountryIsoCode,  // Retrieve CountryIsoCode
                    Country = g.FirstOrDefault().co.CountryName, 
                    TotalSpent = g.Sum(x => x.il.UnitPrice * x.il.Quantity)
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToListAsync();

            return Ok(grouped);
        }

        // GET: api/Tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }

        // GET: api/Tracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id);

            if (track == null)
            {
                return NotFound();
            }

            return track;
        }

    }
}

