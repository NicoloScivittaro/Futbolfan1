using System.ComponentModel.DataAnnotations;

namespace FutbolFan1.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome utente")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Descrizione")]
        public string Bio { get; set; }

        [Display(Name = "Squadra preferita")]
        public string? FavoriteTeam { get; set; }
    }
}
