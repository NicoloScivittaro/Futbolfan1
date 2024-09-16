namespace FutbolFan1.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public decimal Cost { get; set; }
        public decimal Salary { get; set; }
        public string? Role { get; set; }  // Ruolo del giocatore (opzionale)
        public bool IsStarting { get; set; }  // Se il giocatore è titolare

        // Foreign key
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public ICollection<PlayerSave> PlayerSaves { get; set; }

        // Statistiche del giocatore (valori tra 1 e 99)
        public int Speed { get; set; }      // Velocità
        public int Shooting { get; set; }   // Tiro
        public int Passing { get; set; }    // Passaggi
        public int Dribbling { get; set; }  // Dribbling
        public int Defense { get; set; }    // Difesa
        public int Physical { get; set; }   // Fisico

        // Calcola l'Overall Rating come media delle statistiche principali
        public int Overall
        {
            get
            {
                return (Speed + Shooting + Passing + Dribbling + Defense + Physical) / 6;
            }
        }
    }
}
