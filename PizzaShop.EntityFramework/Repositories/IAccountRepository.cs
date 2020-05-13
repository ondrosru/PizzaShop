using PizzaShop.EntityFramework.Entities;

namespace PizzaShop.EntityFramework.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccount(string username);
    }
}
