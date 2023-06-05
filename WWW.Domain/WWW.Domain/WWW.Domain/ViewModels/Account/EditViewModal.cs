using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WWW.Domain.ViewModels.Account
{
    public class EditViewModal
    {
        [Display(Name = "Nick Name")]
        [AllowNull]
        public string? NickName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [AllowNull]
        public string? Email { get; set; }


        [Display(Name = "Introdaction")]
        [AllowNull]
        public string? Introdaction { get; set; }

        [Display(Name = "Avatar")]
        [AllowNull]
        public IFormFile? Avatar { get; set; }

        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Enter Old Password")]
        [PasswordPropertyText]
        public string OldPassword { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter Password")]
        [PasswordPropertyText]
        public string NewPassword { get; set; }

        [Display(Name = "Password Confirmation")]
        [PasswordPropertyText]
        [Compare("NewPassword", ErrorMessage = "Password mismatch")]
        public string PasswordConfirm { get; set; }


    }
}