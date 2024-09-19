using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Models;
using FutbolFan1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutbolFan1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionshipsController : ControllerBase
    {
        private readonly FutbolFanContext _context;

        public ChampionshipsController(FutbolFanContext context)
        {
            _context = context;
        }

        // GET: api/Championships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Championship>>> GetChampionships()
        {
            var championships = await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .ToListAsync();

            return championships;
        }

        // GET: api/Championships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Championship>> GetChampionship(int id)
        {
            var championship = await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (championship == null)
            {
                return NotFound();
            }

            return championship;
        }

        // POST: api/Championships
        [HttpPost]
        public async Task<ActionResult<Championship>> PostChampionship([FromBody] Championship championship)
        {
            if (championship == null)
            {
                return BadRequest("Championship object is null");
            }

            // Aggiungi un controllo di validazione personalizzato qui se necessario
            _context.Championships.Add(championship);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Gestione delle eccezioni nel caso di errori di aggiornamento del database
                return StatusCode(500, $"Error saving championship: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetChampionship), new { id = championship.Id }, championship);
        }

        // PUT: api/Championships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChampionship(int id, [FromBody] Championship championship, [FromQuery] List<int> selectedTeamIds)
        {
            if (id != championship.Id)
            {
                return BadRequest("Championship ID mismatch");
            }

            var existingChampionship = await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingChampionship == null)
            {
                return NotFound();
            }

            // Aggiorna le proprietà del campionato
            existingChampionship.Name = championship.Name;
            existingChampionship.StartDate = championship.StartDate;
            existingChampionship.EndDate = championship.EndDate;

            // Aggiorna la relazione ChampionshipTeams
            existingChampionship.ChampionshipTeams.Clear();
            foreach (var teamId in selectedTeamIds)
            {
                existingChampionship.ChampionshipTeams.Add(new ChampionshipTeam
                {
                    ChampionshipId = existingChampionship.Id,
                    TeamId = teamId
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChampionshipExists(existingChampionship.Id))
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

        // DELETE: api/Championships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChampionship(int id)
        {
            var championship = await _context.Championships.FindAsync(id);
            if (championship == null)
            {
                return NotFound();
            }

            _context.Championships.Remove(championship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChampionshipExists(int id)
        {
            return _context.Championships.Any(e => e.Id == id);
        }
    }
}
