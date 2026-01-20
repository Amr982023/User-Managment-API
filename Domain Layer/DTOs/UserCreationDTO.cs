using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Consts;

namespace Domain_Layer.DTOs
{
    public class UserCreationDTO
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required enUserRole UserRole { get; set; }
        public required DateTime DateOfBirth { get; set; }

    }
}
