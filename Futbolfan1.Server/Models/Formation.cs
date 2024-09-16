namespace FutbolFan1.Models
{
    public class Formation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; } // Associazioni con Team
        public ICollection<Position> Positions { get; set; } // Se ci sono posizioni
    }
}
