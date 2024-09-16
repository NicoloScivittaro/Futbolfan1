namespace FutbolFan1.Models
{
    public class PlayerSave
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }

        // Foreign keys
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int TeamSaveId { get; set; }
        public TeamSave TeamSave { get; set; }
    }



}
