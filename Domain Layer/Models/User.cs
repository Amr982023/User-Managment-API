using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Common;
using Domain_Layer.Consts;
using Domain_Layer.DTOs;

namespace Domain_Layer.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required enUserRole UserRole { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime? UpdatedAt { get; set; }
        public required DateTime DateOfBirth { get; set; }

        public static Result<User> Create(UserCreationDTO Dto)
        {

            try
            {
                if (!(Dto.DateOfBirth <= DateTime.Today.AddYears(-18)))
                {
                    return Result<User>.Failure("User must be at least 18 years old.");
                }

                return Result<User>.Success(new User
                {
                    Username = Dto.Username,
                    PasswordHash = Dto.Password,
                    Email = Dto.Email,
                    CreatedAt = DateTime.Now,
                    DateOfBirth = Dto.DateOfBirth,
                    UserRole = Dto.UserRole,
                    UpdatedAt = null
                });


            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"An error occurred while validating: {ex.Message}");
            }

        }

    }
}
