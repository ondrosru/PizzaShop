using PizzaShopAdmin.Dto.Account;
using System.Collections.Generic;

namespace PizzaShopAdmin.Services
{
    public interface IAccountService
    {
        AccountDto GetAnUsernameAccount(string username);
        AccountDto GetAnEmailAccount(string email);
        List<AccountDto> GetAccounts();
        AccountDto GetAccount(int id);
        AccountDto SaveAccount(AccountDto newAccount);
        void DeleteAccount(int id);
    }
}
