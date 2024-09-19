using System.Text.Json.Serialization;

namespace FutbolFan1.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public decimal TransferBudget { get; set; }
        public decimal SalaryBudget { get; set; }

        // Proprietà per il valore complessivo della squadra
        public decimal Overall { get; set; }

        // Collezione di giocatori associati alla squadra
        public List<Player> Players { get; set; } = new List<Player>();

        // Ignorare le proprietà che causano cicli durante la serializzazione JSON
        [JsonIgnore]
        public ICollection<TeamSave> TeamSaves { get; set; } = new List<TeamSave>();

        // Relazione con ChampionshipTeam
        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; } = new List<ChampionshipTeam>();
    }
}
