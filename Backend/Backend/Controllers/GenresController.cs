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
        public async Task<ActionResult<IEnumerable<StackedGenreDto>>> GetGenresStacked()
        {
            var result = await (
                from il in _context.InvoiceLines
                join inv in _context.Invoices on il.InvoiceId equals inv.InvoiceId
                join cust in _context.Customers on inv.CustomerId equals cust.CustomerId
                join country in _context.Countries on cust.CountryId equals country.CountryId
                join track in _context.Tracks on il.TrackId equals track.TrackId
                join genre in _context.Genres on track.GenreId equals genre.GenreId
                group new { il, country, genre } by new { country.CountryName, genre.Name } into g
                select new StackedGenreDto
                {
                    Country = g.Key.CountryName,
                    Genre = g.Key.Name,
                    TotalSpent = g.Sum(x => x.il.UnitPrice * x.il.Quantity)
                }
            ).ToListAsync();

            return Ok(result);
        }


    }
}
