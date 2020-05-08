using System;
using System.Linq;
using System.Reflection;
using PizzaShop.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.EntityFramework
{
    public class PizzaShopDbContext : DbContext
    {
        private readonly DbContextConfiguration _configuration;

        public PizzaShopDbContext(DbContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PizzaShopDbContext(DbContextOptions<PizzaShopDbContext> options, DbContextConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToRegister = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();


            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}