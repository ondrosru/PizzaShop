using Microsoft.Extensions.Configuration;
using PizzaShop.EntityFramework.Configuration;

namespace PizzaShop.Config
{
        internal class ApplicationConfig
        {
            public DbContextConfiguration DbContextConfiguration => _serviceConfiguration.GetSection("DbContextConfiguration").Get<DbContextConfiguration>();

            private readonly IConfiguration _serviceConfiguration;

            public ApplicationConfig(IConfiguration serviceConfiguration)
            {
                _serviceConfiguration = serviceConfiguration;
            }
        }
}
