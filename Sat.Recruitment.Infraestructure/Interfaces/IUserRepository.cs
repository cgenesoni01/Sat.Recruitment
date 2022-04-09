using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateUserAsync(UserModel user);
        Task<List<UserModel>> GetUsersAsync();
    }
}
