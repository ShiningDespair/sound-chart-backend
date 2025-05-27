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
    /// Controller for managing genres in the Chinook database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ChinookContext _context;
        /// <summary>
        /// Initializes a new instance of the <see cref="GenresController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public GenresController(ChinookContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves a list of all unique genres from the database.
        /// </summary>
        /// <returns>A list of <see cref="Genre"/></returns>
        // GET: api/Genres unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetGenres()
        {
            return await _context.Genres
                .Select(g => g.Name!)
                .Distinct()
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a list of genres with their total sales amount, grouped by genre name for D3.js stacked bar chart.
        /// </summary>
        /// <returns>A list of <see cref="StackedGenreDto"/> objects.</returns>
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
                    Country = il.Invoice.Customer.Country!.CountryName,
                    Genre = il.Track.Genre!.Name,
                    TotalSpent = il.UnitPrice * il.Quantity
                })
                .GroupBy(x => new { x.Country, x.Genre })
                .Select(g => new StackedGenreDto
                {
                    Country = g.Key.Country!,
                    Genre = g.Key.Genre!,
                    TotalSpent = g.Sum(x => x.TotalSpent)
                })
                .ToListAsync();

            return Ok(result);
        }

    }
}
