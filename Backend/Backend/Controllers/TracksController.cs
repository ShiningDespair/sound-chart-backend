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
    /// <summary>
    /// Controller for managing tracks in the Chinook database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ChinookContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracksController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public TracksController(ChinookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of tracks grouped by country for a world map visualization.
        /// </summary>
        /// <param name="genre"> Filters the results according to genre.</param>
        /// <param name="minDuration">Filters the results according to minimum duration.</param>
        /// <param name="maxDuration">Filters the results according to maximum duration.</param>
        /// <param name="artist">Filters the results according to artist.</param>
        /// <param name="album">Filters the results according to album.</param>
        /// <param name="mediaType">Filters the results according to media type.</param>
        /// <returns> A list of filtered <see cref="WorldMapDto"/> objects. </returns>
        [HttpGet("worldMap")]
        public async Task<ActionResult<IEnumerable<WorldMapDto>>> GetTracksMap(
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
                query = query.Where(x => x.ar.Name!.Contains(artist));

            if (!string.IsNullOrEmpty(album))
                query = query.Where(x => x.a.Title.Contains(album));

            if(!string.IsNullOrEmpty(album) && !string.IsNullOrEmpty(artist))
            {
                query = query.Where(x => x.a.Title.Contains(album) && x.ar.Name!.Contains(artist));
            }

            // Group by CountryId, but return the associated CountryIsoCode
            var grouped = (await query
                .GroupBy(x => x.c.CountryId)
                .ToListAsync()) // ← Materialize the query here
                .Select(g =>
                {
                    var first = g.FirstOrDefault();

                    return new WorldMapDto
                    {
                        CountryIsoCode = first?.co.CountryIsoCode ?? 0,
                        Country = first?.co.CountryName ?? "Unknown",
                        TotalSpent = g.Sum(x => x.il.UnitPrice * x.il.Quantity)
                    };
                })
                .OrderByDescending(x => x.TotalSpent)
                .ToList();


            return Ok(grouped);
        }

        /// <summary>
        /// Retrieves a list of all tracks in the database.
        /// </summary>
        /// <returns>A list of <see cref="Track"/> objects. </returns>
        // GET: api/Tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return await _context.Tracks.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific track by its ID.
        /// </summary>
        /// <param name="id">The id of track to be retrieved.</param>
        /// <returns>An object of <see cref="Track"/>.</returns>
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

