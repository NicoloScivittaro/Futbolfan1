namespace FutbolFan1.Models
{
    public class SellPlayerRequest
    {
        public int PlayerId { get; set; }
        public int SellingTeamId { get; set; }
        public int? BuyingTeamId { get; set; }
    }
}
