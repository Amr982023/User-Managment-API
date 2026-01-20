using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Interfaces.IGeneric;
using Domain_Layer.Models;

namespace Domain_Layer.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public string GetUserNameById(int id);
        public string GetRoleByUserId(int id);
    }
}
