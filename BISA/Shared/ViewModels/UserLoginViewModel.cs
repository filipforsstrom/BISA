using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Please input your username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Input password")]
        public string Password { get; set; }
    }
}
