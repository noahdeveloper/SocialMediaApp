using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMediaApp.Models
{
    public class UserInformation
    {
        public string ID { get; set; }

        [Display(Name = "Age")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Whole numbers only")]
        public int Age { get; set; }

        [Display(Name = "Favorite Quote")]
        public string FavoriteQuote { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicName { get; set; }
    }
}