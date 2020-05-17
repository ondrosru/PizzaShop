using PizzaShop.EntityFramework.Entities;
using System.Collections.Generic;

namespace PizzaShop.EntityFramework.Repositories
{
    public interface IPizzaRepository: IRepository<Pizza>
    {
        Pizza GetPizza(int id);
        IEnumerable<Pizza> GetPizzas();
    }
}
