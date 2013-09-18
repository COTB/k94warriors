using System.ComponentModel.DataAnnotations;

namespace K94Warriors.Models.Accounts
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}