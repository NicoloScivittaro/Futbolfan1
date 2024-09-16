
namespace FutbolFan1.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public decimal TransferBudget { get; set; }
        public decimal SalaryBudget { get; set; }
        public int? CurrentFormationId { get; set; }
        public Formation CurrentFormation { get; set; }

        // New property
        public decimal Overall { get; set; }

        // Navigation property
        public ICollection<Player> Players { get; set; }
        public ICollection<TeamSave> TeamSaves { get; set; }
        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; }
    }
}
