namespace FutbolFan1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        // Imposta un valore predefinito per Bio
        public string Bio { get; set; } = string.Empty;
        public string? FavoriteTeam { get; set; }
    }

}
