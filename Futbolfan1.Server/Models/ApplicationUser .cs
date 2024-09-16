using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FutbolFan1.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom property with a data annotation for validation
        [Required]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
