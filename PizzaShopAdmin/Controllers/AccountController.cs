using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Models;
using PizzaShopAdmin.Services;
using System.Collections.Generic;

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAccount")]
        [Authorize(Policy = Policies.Admin)]
        public AccountDto GetAccount(int id)
        {
            return _accountService.GetAccount(id);
        }

        [HttpGet]
        [Route("GetAccounts")]
        [Authorize(Policy = Policies.Admin)]
        public List<AccountDto> GetUsers()
        {
            return _accountService.GetAccounts();
        }

        [HttpPost]
        [Route("SaveAccount")]
        [Authorize(Policy = Policies.Admin)]
        public AccountDto SaveUser([FromBody] AccountDto newAccount)
        {
            return _accountService.GetAccount(newAccount.Username);
        }
    }
}
