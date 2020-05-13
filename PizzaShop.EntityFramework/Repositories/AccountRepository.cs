using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaShop.EntityFramework.Entities;
using System.Linq;

namespace PizzaShop.EntityFramework.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly PizzaShopDbContext _pizzaShopDbContext;
        public AccountRepository(PizzaShopDbContext pizzaShopDbContext) : base(pizzaShopDbContext)
        {
            _pizzaShopDbContext = pizzaShopDbContext;
        }

        public Account GetAccount(string username)
        {
            string command = @"SELECT * FROM dbo.Accounts WHERE @Username = Username";
            SqlParameter parameterUsername = new SqlParameter("@Username", username);
            return Table.FromSqlRaw(command, parameterUsername).FirstOrDefault();
        }
    }
}
