using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttr = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace GamePool.PL.MVC.Models.Account
{
    public class UserRegisterVM
    {
        [Required]
        [Remote("IsUsernameNotExist", "Account", ErrorMessage = "User with that login is exist")]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        [CompareAttr(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}