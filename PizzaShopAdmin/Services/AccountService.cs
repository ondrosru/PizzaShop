using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Entities.Enum;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopAdmin.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountDto GetAnUsernameAccount(string username)
        {
            Account account = _accountRepository.GetAnUsernameAccount(username);
            if (account != null)
            {
                return Convert(account);
            }
            return null;
        }

        public AccountDto GetAnEmailAccount(string email)
        {
            Account account = _accountRepository.GetAnEmailAccount(email);
            if (account != null)
            {
                return Convert(account);
            }
            return null;
        }

        public AccountDto GetAccount(int id)
        {
            if (id == 0)
            {
                return CreateAccount();
            }

            Account account = _accountRepository.GetItem(id);
            if (account != null)
            {
                return Convert(account);
            }
            return null;
        }

        public List<AccountDto> GetAccounts()
        {
            List<Account> accounts = _accountRepository.All.ToList();
            return accounts.ConvertAll(Convert);
        }

        public AccountDto SaveAccount(AccountDto newAccount)
        {
            Account account = _accountRepository.GetItem(newAccount.Id) ?? new Account { };
            account.Name = newAccount.Name;
            account.Surname = newAccount.Surname;
            account.Username = newAccount.Username;
            account.Password = newAccount.Password;
            account.Role = ConvertRole(newAccount.Role);
            account.Address = newAccount.Address;
            account.Email = newAccount.Email;
            account.Phone = newAccount.Phone;
            account.CreatedAt = DateTime.Now;
            account = _accountRepository.Save(account);
            return Convert(account);
        }

        private AccountDto CreateAccount()
        {
            return new AccountDto
            {
                Id = 0,
                Name = string.Empty,
                Surname = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                Address = string.Empty,
                Phone = string.Empty
            };
        }

        private Role ConvertRole(string role)
        {
            Role convertedRole;
            if (role.Equals(Policies.Admin))
            {
                convertedRole = Role.Administrator;
            }
            else
            {
                convertedRole = Role.Client;
            }
            return convertedRole;
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
                convertedAccount.Role = Policies.Client;
            }
            return convertedAccount;
        }

        public void DeleteAccount(int id)
        {
            _accountRepository.Delete(id);
        }
    }
}
