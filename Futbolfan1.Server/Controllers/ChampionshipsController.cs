using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Models;
using FutbolFan1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutbolFan1.Controllers
{
    // Questo controller è dedicato solo alle API
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionshipsApiController : ControllerBase
    {
        private readonly FutbolFanContext _context;

        public ChampionshipsApiController(FutbolFanContext context)
        {
            _context = context;
        }

        // GET: api/Championships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Championship>>> GetChampionships()
        {
            return await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .ToListAsync();
        }


        // POST: api/Championships
        [HttpPost]
        public async Task<ActionResult<Championship>> PostChampionship(Championship championship)
        {
            _context.Championships.Add(championship);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChampionships), new { id = championship.Id }, championship);
        }

        // PUT: api/Championships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChampionship(int id, Championship championship)
        {
            if (id != championship.Id)
            {
                return BadRequest();
            }

            _context.Entry(championship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChampionshipExists(id))
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

        private bool ChampionshipExists(int id)
        {
            return _context.Championships.Any(e => e.Id == id);
        }
    }

    // Questo controller è dedicato alle viste MVC per la gestione dei campionati
    [Route("championships")]
    public class ChampionshipsController : Controller
    {
        private readonly FutbolFanContext _context;

        public ChampionshipsController(FutbolFanContext context)
        {
            _context = context;
        }

        // GET: championships
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var championships = await _context.Championships.ToListAsync();
            return View(championships);
        }
        // GET: api/Championships/Details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var championship = await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (championship == null)
            {
                return NotFound();
            }

            return View(championship);
        }

        // GET: championships/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: championships/Create
        // POST: championships/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Championship championship)
        {
            // Aggiungi direttamente il campionato senza controllare ModelState
            _context.Championships.Add(championship);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // POST: championships/Edit/5
        // GET: championships/Edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var championship = await _context.Championships
                .Include(c => c.ChampionshipTeams)
                .ThenInclude(ct => ct.Team)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (championship == null)
            {
                return NotFound();
            }

            // Passa le squadre disponibili alla vista tramite ViewBag
            ViewBag.AvailableTeams = await _context.Teams.ToListAsync();

            return View(championship);
        }


        // POST: championships/Edit/5
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Championship championship, List<int> SelectedTeamIds)
        {
            if (id != championship.Id)
            {
                return BadRequest();
            }

            var existingChampionship = await _context.Championships
                .Include(c => c.ChampionshipTeams)
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
            foreach (var teamId in SelectedTeamIds)
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

            return RedirectToAction(nameof(Index));
        }
        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var championship = await _context.Championships.FindAsync(id);
            if (championship == null)
            {
                return NotFound();
            }

            _context.Championships.Remove(championship);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool ChampionshipExists(int id)
        {
            return _context.Championships.Any(e => e.Id == id);
        }
    }
}
