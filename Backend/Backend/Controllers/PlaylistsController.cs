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
    public class PlaylistsController : ControllerBase
    {
        private readonly ChinookContext _context;

        public PlaylistsController(ChinookContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _context.Playlists.ToListAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return playlist;
        }

        [HttpGet("Bubble")]
        public async Task<ActionResult<IEnumerable<PlaylistBubbleDto>>> GetBubblePlaylist()
        {
            var result = await _context.Playlists
                .Select(p => new PlaylistBubbleDto
                {
                    Name = p.Name!,
                    Sale = p.Tracks
                        .SelectMany(t => t.InvoiceLines)
                        .Sum(il => il.Quantity * il.UnitPrice)

                })
                .Where(x => x.Sale > 1)
                .ToListAsync();

            return Ok(result);
        }



    }
}

