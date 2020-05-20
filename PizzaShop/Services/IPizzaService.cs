using PizzaShop.Models;
using System.Collections.Generic;

namespace PizzaShop.Services
{
    public interface IPizzaService
    {
        List<PizzaDto> GetPizzas();
        PizzaDto GetPizza(int id);
    }
}
