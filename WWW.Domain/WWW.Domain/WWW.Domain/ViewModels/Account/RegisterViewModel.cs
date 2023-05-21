using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WWW.Domain.Enum;

namespace WWW.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        //[Display(Name = "First Name")]
        //[Required(ErrorMessage = "Enter First Name")]
        //public string FirstName { get; set; }

        //[Display(Name = "Last Name")]
        //[Required(ErrorMessage = "Enter Last Name")]
        //public string LastName { get; set; }

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
