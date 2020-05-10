using System.Collections.Generic;
using PizzaShopAdmin.Dto.User;
using PizzaShopAdmin.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService userService)
        {
            _usersService = userService;
        }

        [HttpGet("users")]
        public List<UserDto> GetUsers()
        {
            return _usersService.GetUsers();
        }
    }
}