using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Data;
using FutbolFan1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FutbolFan1.Controllers
{
    public class PlayersController : Controller
    {
        private readonly FutbolFanContext _context;

        public PlayersController(FutbolFanContext context)
        {
            _context = context;
        }

        // GET: Players
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var players = await _context.Players.Include(p => p.Team).ToListAsync();
            return View(players);
        }
        // GET: api/Teams/{teamId}/players
        [HttpGet("{teamId}/players")]
        public async Task<IActionResult> GetPlayersByTeam(int teamId)
        {
            var players = await _context.Players
                .Where(p => p.TeamId == teamId)
                .ToListAsync();

            return Ok(players);
        }


        // GET: Players/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Position,Age,Cost,Salary,Role,IsStarting,TeamId,Speed,Shooting,Passing,Dribbling,Defense,Physical")] Player player)
        {
            try
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Non è stato possibile salvare il giocatore. Verifica i dati inseriti e riprova.");
                ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
                return View(player);
            }
        }

        // GET: Players/Edit/5
        [HttpGet]
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
        [HttpPost]
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Si è verificato un errore durante l'aggiornamento del giocatore. " + ex.Message);
                ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", player.TeamId);
                return View(player);
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
