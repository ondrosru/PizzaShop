using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PizzaShop.Config;
using PizzaShop.EntityFramework;
using System.IO;

namespace PizzaShop
{
    public class PizzaShopDbContextFactory : IDesignTimeDbContextFactory<PizzaShopDbContext>
    {
        public PizzaShopDbContext CreateDbContext(string[] args)
        {
            ApplicationConfig config = new ApplicationConfig(GetServiceConfiguration());
            return new PizzaShopDbContext(config.DbContextConfiguration);
        }

        private IConfiguration GetServiceConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false);


            string additioonalCofigFile = $"appsettings.json";
            if (File.Exists(additioonalCofigFile))
            {
                configurationBuilder.AddJsonFile(additioonalCofigFile, false);
            }

            return configurationBuilder.Build();
        }
    }
}
