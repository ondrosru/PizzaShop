using PizzaShop.EntityFramework.Entities;

namespace PizzaShop.EntityFramework.Repositories
{
    public interface IPizzaIngredientRepository: IRepository<PizzaIngredient>
    {
        PizzaIngredient GetPizzaIngredient(int pizzaId, int ingredientId);
    }
}
