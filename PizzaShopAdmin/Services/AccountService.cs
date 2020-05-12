using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Entities.Enum;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Models;

namespace PizzaShopAdmin.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public AccountDto GetAccount(LoginDto loginData)
        {
            Account account = _accountRepository.GetAccount(loginData.Username, loginData.Password);
            if (account != null)
            {
                return Convert(account);
            }
            return null;
        }

        private AccountDto Convert(Account account)
        {
            AccountDto convertedAccount = new AccountDto
            {
                Id = account.Id,
                Username = account.Username,
                Password = account.Password,
                Name = account.Name,
                Surname = account.Surname,
                Phone = account.Phone,
                Email = account.Email,
                Address = account.Address,
                CreatedAt = account.CreatedAt
            };
            if (account.Role == Role.Administrator)
            {
                convertedAccount.Role = Policies.Admin;
            }
            else
            {
                convertedAccount.Role = Policies.User;
            }
            return convertedAccount;
        }
    }
}
