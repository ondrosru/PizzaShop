using PizzaShopAdmin.Dto.Account;
using System.Collections.Generic;

namespace PizzaShopAdmin.Services
{
    public interface IAccountService
    {
        AccountDto GetAccount(string username);
        List<AccountDto> GetAccounts();
        AccountDto GetAccount(int id);
        AccountDto SaveAccount(AccountDto newAccount);
    }
}
