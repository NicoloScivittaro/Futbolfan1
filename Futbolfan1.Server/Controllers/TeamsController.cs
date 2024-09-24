using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Data;
using FutbolFan1.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FutbolFan1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly FutbolFanContext _context;

        public TeamsController(FutbolFanContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _context.Teams
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Coach,
                    t.TransferBudget,
                    t.SalaryBudget,
                    t.Overall
                    // Rimuovi questa linea
                    // CurrentFormation = t.CurrentFormation.Name
                })
                .ToListAsync();

            return Ok(teams);
        }




        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            try
            {
                var team = await _context.Teams
                    .Include(t => t.Players)
                    .FirstOrDefaultAsync(t => t.Id == teamId);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("BuyPlayers/{teamId}")]
        public async Task<IActionResult> GetAvailablePlayers(int teamId)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == teamId);
            if (team == null)
            {
                return NotFound();
            }

            var availablePlayers = await _context.Players
                .Where(p => p.TeamId != teamId)
                .ToListAsync();

            return Ok(new { selectedTeam = team, availablePlayers });
        }

        [HttpPost("BuyPlayer")]
        public async Task<IActionResult> BuyPlayer([FromBody] BuyPlayerRequest request)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == request.TeamId);
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId);

            if (team == null || player == null)
            {
                return NotFound();
            }

            // Log per il debug
            Console.WriteLine($"Before Purchase: Team Budget = {team.TransferBudget}, Player Cost = {player.Cost}");

            if (team.TransferBudget < player.Cost || team.SalaryBudget < player.Salary)
            {
                return BadRequest("Insufficient budget to buy this player.");
            }

            team.TransferBudget -= player.Cost;
            team.SalaryBudget -= player.Salary;
            player.TeamId = team.Id;

            // Log dopo l'acquisto
            Console.WriteLine($"After Purchase: Team Budget = {team.TransferBudget}");

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("SellPlayer")]
        public async Task<IActionResult> SellPlayer([FromBody] SellPlayerRequest request)
        {
            // Validazione dell'input
            if (request.SellingTeamId <= 0 || request.PlayerId <= 0)
            {
                return BadRequest("Invalid team or player ID.");
            }

            var sellingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == request.SellingTeamId);
            var player = await _context.Players.FindAsync(request.PlayerId);

            if (player == null || sellingTeam == null || player.TeamId != sellingTeam.Id)
            {
                return NotFound("Player or selling team not found.");
            }

            // Se è specificato un team acquirente
            if (request.BuyingTeamId.HasValue)
            {
                var buyingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == request.BuyingTeamId.Value);

                if (buyingTeam == null)
                {
                    return NotFound("Buying team not found.");
                }

                // Controllo se il team acquirente ha abbastanza budget
                if (buyingTeam.TransferBudget < player.Cost || buyingTeam.SalaryBudget < player.Salary)
                {
                    return BadRequest("The buying team doesn't have enough budget.");
                }

                // Aggiorna i budget
                sellingTeam.TransferBudget += player.Cost;
                buyingTeam.TransferBudget -= player.Cost;

                // Aggiorna il SalaryBudget
                sellingTeam.SalaryBudget += player.Salary; // Aggiungi il salario del giocatore al budget di vendita
                buyingTeam.SalaryBudget -= player.Salary; // Deduci il salario dal budget di acquisto

                // Aggiorna il team del giocatore
                player.TeamId = buyingTeam.Id;
            }
            else
            {
                // Se non c'è un team acquirente, il giocatore viene rilasciato
                sellingTeam.TransferBudget += player.Cost;
                sellingTeam.SalaryBudget += player.Salary; // Aggiungi il salario del giocatore al budget di vendita
                player.TeamId = null; // Il giocatore viene rimosso dalla squadra
            }

            // Salva le modifiche al database
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("SetFormationAndStartingXI")]
        public async Task<IActionResult> SetFormationAndStartingXI([FromBody] SetFormationRequest request)
        {
            // Trova il team in base all'ID
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == request.TeamId);

            if (team == null)
            {
                return NotFound("Team not found.");
            }

            // Trova la formazione selezionata
            var selectedFormation = await _context.Formations.FirstOrDefaultAsync(f => f.Name == request.Formation);

            if (selectedFormation == null)
            {
                return BadRequest("Invalid formation selected.");
            }

            // Imposta i giocatori titolari e i loro ruoli
            foreach (var player in team.Players)
            {
                player.IsStarting = request.StartingXI.Contains(player.Id);

                if (request.PlayerRole.ContainsKey(player.Id))
                {
                    player.Role = request.PlayerRole[player.Id];
                }
            }

            // Salva le modifiche al database
            await _context.SaveChangesAsync();

            return Ok("Formation and Starting XI successfully updated.");
        }


        // Endpoint per salvare lo stato attuale della squadra
        [HttpPost("SaveTeam")]
        public async Task<IActionResult> SaveTeam([FromBody] SaveTeamRequest request)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == request.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            var teamSave = new TeamSave
            {
                SaveName = request.SaveName,
                TeamId = request.TeamId,
                TransferBudget = team.TransferBudget,
                SalaryBudget = team.SalaryBudget,
                SavedAt = DateTime.Now,
                PlayerSaves = team.Players.Select(p => new PlayerSave
                {
                    PlayerId = p.Id,
                    Name = p.Name,
                    Position = p.Position,
                    Age = p.Age
                }).ToList()
            };

            _context.TeamSaves.Add(teamSave);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("LoadTeamSave/{saveId}")]
        public async Task<IActionResult> LoadTeamSave(int saveId)
        {
            try
            {
                // Log di debug
                Console.WriteLine($"Caricamento del salvataggio con ID: {saveId}");

                var teamSave = await _context.TeamSaves
                    .Include(ts => ts.PlayerSaves)
                    .FirstOrDefaultAsync(ts => ts.Id == saveId);

                if (teamSave == null)
                {
                    Console.WriteLine("Salvataggio non trovato");
                    return NotFound();
                }

                var team = await _context.Teams
                    .Include(t => t.Players)
                    .FirstOrDefaultAsync(t => t.Id == teamSave.TeamId);

                if (team == null)
                {
                    Console.WriteLine("Squadra non trovata");
                    return NotFound();
                }

                Console.WriteLine("Salvataggio trovato e caricato correttamente");

                return Ok(team);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il caricamento del salvataggio: {ex.Message}");
                return StatusCode(500, "Errore durante il caricamento del salvataggio.");
            }
        }

        [HttpGet("Formations")]
        public IActionResult GetFormations()
        {
            var formations = new List<Formation>
            {
                new Formation
                {
                    Name = "3-4-2-1",
                    Positions = new List<Position>
                    {
                        new Position { Id = 1, X = 5, Y = 17 },
                        new Position { Id = 2, X = 65, Y = 90 },
                        new Position { Id = 3, X = 65, Y = 157 },
                        new Position { Id = 4, X = 65, Y = 224 },
                        new Position { Id = 5, X = 114, Y = 53 },
                        new Position { Id = 6, X = 114, Y = 126 },
                        new Position { Id = 7, X = 114, Y = 199 },
                        new Position { Id = 8, X = 227, Y = 90 },
                        new Position { Id = 9, X = 227, Y = 157 },
                        new Position { Id = 10, X = 338, Y = 108 }
                    }
                }
            };

            return Ok(formations);
        }

        [HttpPost("api/teams/create")]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest("Team object is null");
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] Team team)
        {
            if (id != team.Id)
            {
                return BadRequest("Team ID mismatch");
            }

            var existingTeam = await _context.Teams.FindAsync(id);
            if (existingTeam == null)
            {
                return NotFound("Team not found");
            }

            // Update team properties
            existingTeam.Name = team.Name;
            existingTeam.TransferBudget = team.TransferBudget;
            existingTeam.SalaryBudget = team.SalaryBudget;
            existingTeam.Coach = team.Coach;
            existingTeam.Overall = team.Overall;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingTeam);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound("Team not found during update");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }

        [HttpGet("GetSaves")]
        public async Task<IActionResult> GetSaves()
        {
            var saves = await _context.TeamSaves
                .Select(ts => new
                {
                    Id = ts.Id,
                    Name = ts.SaveName,
                    TeamId = ts.TeamId,
                    SavedAt = ts.SavedAt
                })
                .ToListAsync();

            return Ok(saves);
        }
        [HttpPut("RenameSave/{saveId}")]
        public async Task<IActionResult> RenameSave(int saveId, [FromBody] RenameSaveRequest request)
        {
            var teamSave = await _context.TeamSaves.FindAsync(saveId);
            if (teamSave == null)
            {
                return NotFound("Save not found.");
            }

            teamSave.SaveName = request.NewName;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Definisci la classe di richiesta
        public class RenameSaveRequest
        {
            public string NewName { get; set; }
        }
        [HttpDelete("DeleteSave/{id}")]
        public async Task<IActionResult> DeleteSave(int id)
        {
            var teamSave = await _context.TeamSaves.FindAsync(id);
            if (teamSave == null)
            {
                return NotFound("Save not found.");
            }

            Console.WriteLine($"Attempting to delete save with ID: {id}");

            _context.TeamSaves.Remove(teamSave);

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"Successfully deleted save with ID: {id}");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database update error: {dbEx.Message}");
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting save: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok();
        }



    }

}