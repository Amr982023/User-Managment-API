using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTOs;
using Domain_Layer.Models;

namespace Application_Layer.Interfaces.Security
{
    public interface ITokenGenerator
    {
        string GenerateToken(User User);
    }
}
