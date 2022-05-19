using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public int Warnings { get; set; }
    }
}
