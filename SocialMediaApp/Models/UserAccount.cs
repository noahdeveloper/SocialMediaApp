using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Models
{
    public class UserAccount
    {
        public int ID { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Password Confirm")]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        public string ConfimPassword { get; set; }

        public string IPAddressCreatedAccountDevice { get; set; }
    }

    public class SignInViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}