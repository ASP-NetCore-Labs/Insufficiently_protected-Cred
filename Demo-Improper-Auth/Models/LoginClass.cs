using System.ComponentModel.DataAnnotations;

namespace Demo_Improper_Auth.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Enter Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
    }
}
