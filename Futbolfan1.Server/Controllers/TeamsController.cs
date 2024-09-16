using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolFan1.Data;
using FutbolFan1.Models;
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
                .Include(t => t.TeamSaves)
                .Include(t => t.CurrentFormation)
                .ToListAsync();

            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeam(int teamId)
        {
            var team = await _context.Teams
                .Include(t => t.Players)
                .Include(t => t.CurrentFormation)
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
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

            if (team.TransferBudget < player.Cost || team.SalaryBudget < player.Salary)
            {
                return BadRequest("Insufficient budget to buy this player.");
            }

            team.TransferBudget -= player.Cost;
            team.SalaryBudget -= player.Salary;
            player.TeamId = team.Id;

            UpdateTeamOverall(team);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("SellPlayer")]
        public async Task<IActionResult> SellPlayer([FromBody] SellPlayerRequest request)
        {
            var sellingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == request.SellingTeamId);
            var player = await _context.Players.FindAsync(request.PlayerId);

            if (player == null || sellingTeam == null || player.TeamId != sellingTeam.Id)
            {
                return NotFound();
            }

            var buyingTeam = request.BuyingTeamId.HasValue
                ? await _context.Teams.FirstOrDefaultAsync(t => t.Id == request.BuyingTeamId.Value)
                : null;

            if (buyingTeam != null)
            {
                if (buyingTeam.TransferBudget < player.Cost || buyingTeam.SalaryBudget < player.Salary)
                {
                    return BadRequest("The buying team doesn't have enough budget.");
                }

                sellingTeam.TransferBudget += player.Cost;
                buyingTeam.TransferBudget -= player.Cost;
                buyingTeam.SalaryBudget -= player.Salary;

                player.TeamId = buyingTeam.Id;
            }
            else
            {
                sellingTeam.TransferBudget += player.Cost;
                player.TeamId = null;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("SetFormationAndStartingXI")]
        public async Task<IActionResult> SetFormationAndStartingXI([FromBody] SetFormationRequest request)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == request.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            var selectedFormation = await _context.Formations.FirstOrDefaultAsync(f => f.Name == request.Formation);

            if (selectedFormation == null)
            {
                return BadRequest("Invalid formation selected.");
            }

            team.CurrentFormation = selectedFormation;

            foreach (var player in team.Players)
            {
                player.IsStarting = request.StartingXI.Contains(player.Id);
                if (request.PlayerRole.ContainsKey(player.Id))
                {
                    player.Role = request.PlayerRole[player.Id];
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

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
            var teamSave = await _context.TeamSaves
                .Include(ts => ts.PlayerSaves)
                .FirstOrDefaultAsync(ts => ts.Id == saveId);

            if (teamSave == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == teamSave.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            team.TransferBudget = teamSave.TransferBudget;
            team.SalaryBudget = teamSave.SalaryBudget;

            team.Players.Clear();
            foreach (var playerSave in teamSave.PlayerSaves)
            {
                team.Players.Add(new Player
                {
                    Id = playerSave.PlayerId,
                    Name = playerSave.Name,
                    Position = playerSave.Position,
                    Age = playerSave.Age,
                    TeamId = team.Id
                });
            }

            await _context.SaveChangesAsync();

            return Ok(team);
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

        private void UpdateTeamOverall(Team team)
        {
            if (team.Players != null && team.Players.Count > 0)
            {
                team.Overall = (decimal)team.Players.Average(p => p.Overall);
            }
        }
    }
}