using System.Text.Json.Serialization;

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
        public string Role { get; set; }
        public bool IsStarting { get; set; }

        // TeamId è facoltativo, per gestire giocatori senza squadra
        public int? TeamId { get; set; }

        // Associare un giocatore a una squadra
        [JsonIgnore]  // Evita cicli di serializzazione
        public Team Team { get; set; }

        // Attributi delle abilità del giocatore
        public decimal Speed { get; set; }
        public decimal Shooting { get; set; }
        public decimal Passing { get; set; }
        public decimal Dribbling { get; set; }
        public decimal Defense { get; set; }
        public decimal Physical { get; set; }
    }
}
