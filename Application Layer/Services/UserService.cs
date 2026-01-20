using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTOs;
using Application_Layer.Interfaces;
using Application_Layer.Interfaces.Security;
using Domain_Layer.Common;
using Domain_Layer.Consts;
using Domain_Layer.DTOs;
using Domain_Layer.Interfaces;
using Domain_Layer.Interfaces.IUOW;
using Domain_Layer.Models;
using Mapster;

namespace Application_Layer.Services
{
    public class UserService : IUserService
    {
        
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _TokenGenerator;
        private readonly IUnitOfWork _UnitOfWork;

        public UserService(
            IUnitOfWork UnitOfWork,
            IPasswordHasher passwordHasher,
            ITokenGenerator TokenGenerator)
        {
            _UnitOfWork = UnitOfWork;
            _passwordHasher = passwordHasher;
            _TokenGenerator = TokenGenerator;
        }

        // Registration
        public async Task<int> RegisterAsync(RegisterUserDto dto)
        {
            // Hash password
            var passwordHash = _passwordHasher.Hash(dto.Password);

            var userDto = new UserCreationDTO
            {
                Username = dto.Username,
                Email = dto.Email,
                UserRole = (enUserRole)dto.UserRole,
                PasswordHash = passwordHash,
                DateOfBirth = dto.DateOfBirth
            }; 

            var user = User.Create(userDto);
           
            if (user.Isfailure)
                throw new Exception(user._ErrorMessage);

            await _UnitOfWork.Users.AddAsync(user._Value);
            await _UnitOfWork.CompleteAsync();

            return user._Value.Id;
        }

        // Login + JWT
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _UnitOfWork.Users
                .FindAsync(u => u.Username == dto.UserName);

            if (user == null)
                return null;

            var validPassword = _passwordHasher.Verify(dto.Password, user.PasswordHash);
            if (!validPassword)
                return null;

            return _TokenGenerator.GenerateToken(user);
        }

        // Read
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _UnitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = Convert.ToInt32(user.UserRole)
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _UnitOfWork.Users.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = Convert.ToInt32(u.UserRole)
            });
        }

        // Update
        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _UnitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                return false;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.UpdatedAt = DateTime.UtcNow;

            _UnitOfWork.Users.Update(user);
            await _UnitOfWork.CompleteAsync();

            return true;
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _UnitOfWork.Users.GetByIdAsync(id);
            if (user == null)
               return false;

            _UnitOfWork.Users.Delete(user);
            await _UnitOfWork.CompleteAsync();

            return true;
        }
    }

}
