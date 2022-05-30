using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Please input your first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please input your last name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please select a username name")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Please input your email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please select a password ")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "You need to confirm your password")]
        [Compare(nameof(Password),ErrorMessage ="Passwords dont match")]
        public string? ValidatePassword { get; set; }


    }
}
