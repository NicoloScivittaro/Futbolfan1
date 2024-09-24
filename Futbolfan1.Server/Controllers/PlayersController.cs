using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Data;
using FutbolFan1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace FutbolFan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly FutbolFanContext _context;

        public PlayersController(FutbolFanContext context)
        {
            _context = context;
        }

        // GET: api/players
        [HttpGet("players")]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();
            return Ok(players);
        }

        // GET: api/players/{id}
        [HttpGet("players/{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var player = await _context.Players.Include(p => p.Team).FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

        // GET: api/teams/{teamId}/players
        [HttpGet("teams/{teamId}/players")]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();

            if (players == null || !players.Any())
            {
                return NotFound();
            }

            return Ok(players);
        }

        // POST: api/players/create
        [HttpPost("players/create")]
        public async Task<IActionResult> CreatePlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Player data is invalid.");
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }

        // PUT: api/players/{id}
        [HttpPut("players/{id}")]
        public async Task<IActionResult> UpdatePlayer(int id, [FromBody] Player player)
        {
            if (id != player.Id)
            {
                return BadRequest("Player ID mismatch.");
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: Players/Edit/5 (for view purposes)
        [HttpGet("players/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost("players/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Position,Age,Cost,Salary,Role,IsStarting,TeamId,Speed,Shooting,Passing,Dribbling,Defense,Physical")] Player player)
        {
            if (id != player.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(player);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(player.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // Metodo privato per verificare l'esistenza di un giocatore
        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
