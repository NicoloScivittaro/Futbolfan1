using FutbolFan1.Models;

public class ChampionshipTeam
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public int ChampionshipId { get; set; }
    public Team Team { get; set; }
    public Championship Championship { get; set; }
}
