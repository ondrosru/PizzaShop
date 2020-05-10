using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto.User;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopAdmin.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<Account> _userRepository;

        public UsersService(IRepository<Account> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDto> GetUsers()
        {
            List<Account> users = _userRepository.All.ToList();

            return users.ConvertAll(Convert);
        }

        private UserDto Convert(Account account)
        {
            return new UserDto
            {
                Id = account.Id,
                Username = account.Username,
                Name = account.Name,
                Surname = account.Surname,
                Birthday = account.Birthday,
                Phone = account.Phone,
                Address = account.Address,
                Email = account.Email,
                Role = account.Role,
                CreatedAt = account.CreatedAt
            };
        }
    }
}
