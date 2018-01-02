using System.ComponentModel.DataAnnotations;

namespace GamePool.PL.MVC.Models.Account
{
    public class UserLoginVm
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public bool? IsExist { get; set; }
    }
}