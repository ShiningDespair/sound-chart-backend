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
    /// <summary>
    /// Controller for managing media types in the Chinook database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MediaTypesController : ControllerBase
    {
        private readonly ChinookContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTypesController"/> class with the specified database context.
        /// </summary>
        /// <param name="context"></param>
        public MediaTypesController(ChinookContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all unique media types from the database.
        /// </summary>
        /// <returns>A list of <see cref="MediaType"/> objects. </returns>
        // GET: api/MediaTypes unique
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaType>>> GetMediaTypes()
        {
            return await _context.MediaTypes.Distinct().ToListAsync();
        }

    }
}
