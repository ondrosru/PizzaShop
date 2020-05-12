using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Services;

namespace PizzaShopAdmin.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("signIn")]
        public AccountDto SignIn([FromBody] LoginDto loginData)
        {
            return _accountService.GetAccount(loginData);
        }
    }
}
