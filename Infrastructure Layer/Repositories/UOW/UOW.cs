using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Interfaces;
using Domain_Layer.Interfaces.IUOW;
using Infrastructure_Layer.Repositories.Repos;

namespace Infrastructure_Layer.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserManagmentDbContext _context;

        public UnitOfWork(UserManagmentDbContext context)
        {
            _context = context;
        }

        private IUser _User;
        public IUser Users =>
            _User ??= new UserRepository(_context);

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
