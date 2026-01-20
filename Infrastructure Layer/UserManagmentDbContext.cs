using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer
{
    public class UserManagmentDbContext : DbContext
    {
        public UserManagmentDbContext(DbContextOptions<UserManagmentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagmentDbContext).Assembly);
        }


    }
}
