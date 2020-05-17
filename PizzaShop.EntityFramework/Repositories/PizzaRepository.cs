using Microsoft.EntityFrameworkCore;
using PizzaShop.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShop.EntityFramework.Repositories
{
    public class PizzaRepository : GenericRepository<Pizza>, IPizzaRepository
    {
        public PizzaRepository(PizzaShopDbContext pizzaShopDbContext) : base(pizzaShopDbContext)
        {
        }
        public Pizza GetPizza(int id)
        {
            Pizza pizza = All.Where(value => value.Id == id)
                .Include(pizza => pizza.Prices)
                .Include(pizza => pizza.PizzaIngredients)
                .ThenInclude(ingredient => ingredient.Ingredient)
                .FirstOrDefault();
            return pizza;
        }

        public IEnumerable<Pizza> GetPizzas()
        {
            IEnumerable<Pizza> pizzas = All.Include(pizza => pizza.Prices)
                .Include(pizza => pizza.PizzaIngredients)
                .ThenInclude(ingredient => ingredient.Ingredient);
            return pizzas;
        }
    }
}
