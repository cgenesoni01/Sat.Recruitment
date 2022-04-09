using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recuitment.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> SaveUserAsync(UserModel user);
    }
}
