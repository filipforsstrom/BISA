using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class UserRoleDTO
    {
        [Required]
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

    }
}
