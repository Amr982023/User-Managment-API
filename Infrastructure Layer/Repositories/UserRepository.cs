using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Infrastructure_Layer.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserManagmentDbContext context) : base(context)
        {
        }

        public string? GetUserNameById(int id)
        {
            return _context.Set<User>()
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => u.Username)
                .FirstOrDefault();
        }

        public string? GetRoleByUserId(int id)
        {
            return _context.Set<User>()
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => u.UserRole.ToString())
                .FirstOrDefault();
        }
    }
}
