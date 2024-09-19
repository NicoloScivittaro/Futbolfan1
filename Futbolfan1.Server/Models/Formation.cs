using FutbolFan1.Models;

public class Formation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Position> Positions { get; set; } = new List<Position>();
    public List<Team> Teams { get; set; } = new List<Team>();
}
