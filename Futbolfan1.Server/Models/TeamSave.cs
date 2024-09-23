using FutbolFan1.Models;

namespace FutbolFan1.Models
{
    public class TeamSave
    {
        public int Id { get; set; }
        public string SaveName { get; set; }
        public int TeamId { get; set; }
        public DateTime SavedAt { get; set; }
        public decimal TransferBudget { get; set; }  // Aggiungi questa propriet�
        public decimal SalaryBudget { get; set; }    // Aggiungi questa propriet�

        public ICollection<PlayerSave> PlayerSaves { get; set; }
        public Team Team { get; set; }
    }



}
