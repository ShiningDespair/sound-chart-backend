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
    public class GenresController : ControllerBase
    {
        private readonly ChinookContext _context;

        public GenresController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/Genres unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetGenres()
        {
            return await _context.Genres
                .Select(g => g.Name!)
                .Distinct()
                .ToListAsync();
        }

        [HttpGet("stacked")]
        public async Task<ActionResult<IEnumerable<object>>> GetGenresStacked()
        {
            var result = await _context.InvoiceLines
                .Where(il =>
                    il.Invoice != null &&
                    il.Invoice.Customer != null &&
                    il.Invoice.Customer.Country != null &&
                    il.Track != null &&
                    il.Track.Genre != null
                )
                .Select(il => new
                {
                    Country = il.Invoice.Customer.Country.CountryName,
                    Genre = il.Track.Genre.Name,
                    TotalSpent = il.UnitPrice * il.Quantity
                })
                .GroupBy(x => new { x.Country, x.Genre })
                .Select(g => new
                {
                    Country = g.Key.Country,
                    Genre = g.Key.Genre,
                    TotalSpent = g.Sum(x => x.TotalSpent)
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

    }
}
