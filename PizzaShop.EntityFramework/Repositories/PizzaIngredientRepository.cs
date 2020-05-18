using PizzaShop.EntityFramework.Entities;
using System.Linq;

namespace PizzaShop.EntityFramework.Repositories
{
    public class PizzaIngredientRepository : GenericRepository<PizzaIngredient>, IPizzaIngredientRepository
    {
        public PizzaIngredientRepository(PizzaShopDbContext pizzaShopDbContext) : base(pizzaShopDbContext)
        {
        }

        public PizzaIngredient GetPizzaIngredient(int pizzaId, int ingredientId)
        {
            return All.Where(value => value.PizzaId == pizzaId && value.IngredientId == ingredientId).FirstOrDefault();
        }
    }
}
