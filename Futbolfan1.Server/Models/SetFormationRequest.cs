namespace FutbolFan1.Models
{
    public class SetFormationRequest
    {
        public int TeamId { get; set; }
        public string Formation { get; set; }
        public List<int> StartingXI { get; set; }
        public Dictionary<int, string> PlayerRole { get; set; }
    }
}
