using System.ComponentModel.DataAnnotations;

namespace Demo_Improper_Auth.Models
{
    public class UserReg
    {
        [Key]
        public int Userid { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter email")]
        public string Uemail { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string Pwd { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [DataType(DataType.Password)]
        [Compare("Pwd")]
        public string ConfirmPwd { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select gender")]
        public char Gender { get; set; }

        public string hashedpwd { get; set; }
    }
}
