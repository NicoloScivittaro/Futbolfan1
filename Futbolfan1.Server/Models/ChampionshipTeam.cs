namespace FutbolFan1.Models
{
    public class Championship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation properties
        public ICollection<ChampionshipTeam> ChampionshipTeams { get; set; }
    }



}
