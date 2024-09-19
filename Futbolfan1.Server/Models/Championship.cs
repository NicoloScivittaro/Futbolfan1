using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FutbolFan1.Models
{
    public class Championship
    {
        [Key]
        [Column("ChampionshipId")] // Questo indica che nel database la colonna si chiama 'ChampionshipId'
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        // Aggiungi i campi StartDate e EndDate
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; }
    }
}
