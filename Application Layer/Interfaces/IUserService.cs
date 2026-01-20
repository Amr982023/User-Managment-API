using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTOs;
using Domain_Layer.Common;

namespace Application_Layer.Interfaces
{
    public interface IUserService
    {

        Task<Result<UserDto>> RegisterAsync(RegisterUserDto dto);

        Task<string?> LoginAsync(LoginDto dto);

        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();


        Task<bool> UpdateAsync(int id, UpdateUserDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
