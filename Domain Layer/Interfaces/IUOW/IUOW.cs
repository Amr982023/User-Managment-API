using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Domain_Layer.Models;

namespace Domain_Layer.Interfaces.IUOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUser Users { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
