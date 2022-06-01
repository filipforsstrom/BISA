using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class UserChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "New password doesn't match")]
        public string ValidatedPassword { get; set; }
    }
}