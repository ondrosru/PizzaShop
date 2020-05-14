using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PizzaShop.EntityFramework.Entities;
using System.Linq;

namespace PizzaShop.EntityFramework.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(PizzaShopDbContext pizzaShopDbContext) : base(pizzaShopDbContext)
        {
        }

        public Account GetAnUsernameAccount(string username)
        {
            string command = @"SELECT * FROM dbo.Accounts WHERE @Username = Username";
            SqlParameter parameterUsername = new SqlParameter("@Username", username);
            return Table.FromSqlRaw(command, parameterUsername).FirstOrDefault();
        }

        public Account GetAnEmailAccount(string email)
        {
            string command = @"SELECT * FROM dbo.Accounts WHERE @Email = Email";
            SqlParameter parameterUsername = new SqlParameter("@Email", email);
            return Table.FromSqlRaw(command, parameterUsername).FirstOrDefault();
        }
    }
}
