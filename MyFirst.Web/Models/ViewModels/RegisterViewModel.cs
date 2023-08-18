using System.ComponentModel.DataAnnotations;

namespace MyFirst.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password has to be at least 6 characters and to contain at least 1 symbol")]
        public string Password { get; set; }
    }
}
