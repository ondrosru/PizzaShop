using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Dto.Error;
using PizzaShopAdmin.Models;
using PizzaShopAdmin.Services;
using System.Collections;
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
        public IActionResult SaveUser([FromBody] AccountDto newAccount)
        {
            AccountDto accountEmail = null;
            AccountDto accountUsername = null;
            if (newAccount.Email != string.Empty)
            {
                accountEmail = _accountService.GetAnEmailAccount(newAccount.Email);
            }
            if (newAccount.Username != string.Empty)
            {
                accountUsername = _accountService.GetAnUsernameAccount(newAccount.Username);
            }
            AccountDto accountDto = newAccount;
            List<ErrorDto> errorsDto = new List<ErrorDto>();
            if (accountEmail == null && accountUsername == null)
            {
                accountDto = _accountService.SaveAccount(newAccount);
            }
            if (accountEmail == null && accountUsername != null)
            {
                if (accountUsername.Id == newAccount.Id)
                {
                    accountDto = _accountService.SaveAccount(newAccount);
                }
                else
                {
                    errorsDto.Add(new ErrorDto("Username", "Данное имя пользователя занято"));
                }
            }
            if (accountEmail != null && accountUsername == null)
            {
                if (accountEmail.Id == newAccount.Id)
                {
                    accountDto = _accountService.SaveAccount(newAccount);
                }
                else
                {
                    errorsDto.Add(new ErrorDto("Email", "Данное email занят"));
                }
            }
            return Ok(new
            {
                account = accountDto,
                errors = errorsDto
            });
        }

        [HttpPost]
        [Route("DeleteAccount")]
        [Authorize(Policy = Policies.Admin)]
        public void DeleteAccount([FromBody] int id)
        {
            _accountService.DeleteAccount(id);
        }
    }
}
