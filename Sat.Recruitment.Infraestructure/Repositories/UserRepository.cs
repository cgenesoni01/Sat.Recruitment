using Sat.Recruitment.Domain;
using Sat.Recruitment.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sat.Recruitment.Domain.Exceptions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<bool> CreateUserAsync(UserModel user)
        {
            var users = await this.GetUsersAsync();

            if (users.Any(u => (u.Email == user.Email || u.Phone == user.Phone) || u.Name == user.Name))
            {
                new UserException("User is duplicated");
            }

            //user created
            return true;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var reader = ReadUsersFromFile();
            var users = new List<UserModel>();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = new UserModel
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                users.Add(user);
            }
            reader.Close();
            return users;
        }
    }
}
