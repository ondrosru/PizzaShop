using PizzaShopAdmin.Dto.User;
using System.Collections.Generic;

namespace PizzaShopAdmin.Services
{
    public interface IUsersService
    {
        List<UserDto> GetUsers();
    }
}
