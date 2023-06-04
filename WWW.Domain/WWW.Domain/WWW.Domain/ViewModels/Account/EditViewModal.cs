using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WWW.Domain.ViewModels.Account
{
    public class EditViewModal
    {
        [Display(Name = "Nick Name")]
        [Required(ErrorMessage = "Enter Nick Name")]
        public string NickName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter Password")]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Display(Name = "Password Confirmation")]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Introdaction")]
        public string? Introdaction { get; set; }

        [Display(Name = "Avatar")]
        [AllowNull]
        public IFormFile? Avatar { get; set; }


    }
}