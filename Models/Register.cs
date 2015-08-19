using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mvcForumapp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "username required")]
        [Display(Name = "Choose username")]
        [StringLength(20, MinimumLength = 4)]
        [Remote("IsUserNameAvailable", "Register", "Username is already taken")]
        public string Username { get; set; }

        [Required(ErrorMessage = "display required")]
        [StringLength(20, MinimumLength = 4)]
        [Remote("IsDisplayAvailable", "Register", "displayname already taken")]
        public string Displayname { get; set; }

        [Required(ErrorMessage = "EmailAddress required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        [Remote("IsEmailAvailable", "Register", "EmailAddress is already taken")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "password required")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "password didn't match")]
        [StringLength(20, MinimumLength = 8)]
        public string PasswordConfirm { get; set; }
    }
}