using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTOs
{
    public class RegisterUserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required int UserRole { get; set; }
        public required string Password { get; set; } 
        public required string ConfirmPassword { get; set; } 
        public required DateTime DateOfBirth { get; set; }
    }
}
