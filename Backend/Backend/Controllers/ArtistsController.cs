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
    /// Controller for managing artists in the Chinook database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ChinookContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistsController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public ArtistsController(ChinookContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Retrieves all artists
        /// </summary>
        /// <returns> A list of <see cref="Artist"/> objects.</returns>
        // GET: api/Artists unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.Distinct().ToListAsync();
        }
        /// <summary>
        /// Retrieves a list of artists with their total sales amount, grouped by artist name for D3.js word cloud.
        /// </summary>
        /// <returns>A list of <see cref="Artist"/> objects.</returns>
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
