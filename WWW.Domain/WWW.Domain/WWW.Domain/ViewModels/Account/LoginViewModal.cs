using System.ComponentModel.DataAnnotations;

namespace WWW.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter name")]
        [MaxLength(20, ErrorMessage = "Name neet to be no longer than 20 symbols")]
        [MinLength(3, ErrorMessage = "Name neet to be no shorter than 20 symbols")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}