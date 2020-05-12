using PizzaShop.EntityFramework.Entities;
using System.Linq;

namespace PizzaShop.EntityFramework.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(PizzaShopDbContext pizzaShopDbContext) : base(pizzaShopDbContext)
        {
        }

        public Account GetAccount(string username, string password)
        {
            return All.Where(item => item.Username == username && item.Password == password).FirstOrDefault();
        }
    }
}
