using PizzaShopAdmin.Dto.Account;

namespace PizzaShopAdmin.Services
{
    public interface IAccountService
    {
        AccountDto GetAccount(LoginDto loginData);
    }
}
