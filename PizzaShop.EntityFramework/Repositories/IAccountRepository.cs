using PizzaShop.EntityFramework.Entities;

namespace PizzaShop.EntityFramework.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAnUsernameAccount(string username);
        Account GetAnEmailAccount(string email);
    }
}
