using Sat.Recruitment.Domain;
using Sat.Recruitment.Infraestructure.Interfaces;
using Sat.Recuitment.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sat.Recuitment.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task<bool> SaveUserAsync(UserModel user)
        {
            if (user.Money > 100)
            {
                var percentage = CalculatePercentage(user);
                //If new user is normal and has more than USD100
                var gif = user.Money * percentage;
                user.Money += gif;
            }

            user.Email = NormalizeEmail(user.Email);

            return await UserRepository.CreateUserAsync(user);
        }

        private decimal CalculatePercentage(UserModel user)
        {
            switch(user.UserType)
            {
                case "Normal":
                    return 0.12m;
                case "SuperUser":
                    return 0.20m;
                case "Premium":
                    return 2;
                default:
                    return 0;
            }
        }

        private string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
