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
    public class AlbumsController : ControllerBase
    {
        private readonly ChinookContext _context;

        public AlbumsController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
        {
            return await _context.Albums.ToListAsync();
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.AlbumId == id);
        }
    

    //GET: api/albums/artistName

    [HttpGet("artistName")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsByArtistName([FromQuery] string? artistName)
        {
            var query = from a in _context.Albums
                        join ar in _context.Artists on a.ArtistId equals ar.ArtistId
                        where ar.Name!.Contains(artistName!)
                        select new AlbumsByArtists { 
                            Album = a.Title
                        };
            
            var result = await query.ToListAsync();

            if (!result.Any())
                return NotFound("No albums found for the given artist name.");

            return Ok(result);


        }

    }
}