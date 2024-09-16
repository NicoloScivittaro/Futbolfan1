namespace FutbolFan1.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }  // Nome del giocatore
        public string Role { get; set; }  // Ruolo del giocatore (ad esempio, Attaccante, Difensore)
        public int X { get; set; }  // Coordinata X sul campo
        public int Y { get; set; }  // Coordinata Y sul campo
    }
}
