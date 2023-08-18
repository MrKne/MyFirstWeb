using System.ComponentModel.DataAnnotations;
using CloudinaryDotNet.Actions;

namespace MyFirst.Web.Models.ViewModels
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6,ErrorMessage="Password must be atleast 6 characters")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
