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

        public Account GetAccount(string username, string password)
        {
            string command = @"SELECT * FROM dbo.Accounts WHERE @Username = Username AND @Password = Password";
            SqlParameter parameterUsername = new SqlParameter("@Username", username);
            SqlParameter parameterPassword = new SqlParameter("@Password", password);
            return Table.FromSqlRaw(command, parameterUsername, parameterPassword).FirstOrDefault();
        }
    }
}
