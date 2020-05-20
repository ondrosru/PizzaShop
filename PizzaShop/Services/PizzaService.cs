using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShop.Services
{
    public class PizzaService: IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public List<PizzaDto> GetPizzas()
        {
            return _pizzaRepository.GetPizzas().ToList().ConvertAll(ConvertPizza);
        }

        public PizzaDto GetPizza(int id)
        {
            if (id == 0)
            {
                PizzaDto newPizza = new PizzaDto();
                newPizza.Prices = new List<PizzaPriceDto>();
                newPizza.Ingredients = new List<PizzaIngredientDto>();
                newPizza.ImgPath = "";
                return newPizza;
            }
            Pizza pizza = _pizzaRepository.GetPizza(id);
            return ConvertPizza(pizza);
        }

        private PizzaDto ConvertPizza(Pizza pizza)
        {
            PizzaDto convertedPizza = new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                ImgPath = pizza.ImgPath,
                Prices = pizza.Prices.ToList().ConvertAll(ConverPrice)
            };
            convertedPizza.Ingredients = new List<PizzaIngredientDto>();
            foreach (PizzaIngredient pizzaIngredient in pizza.PizzaIngredients)
            {
                convertedPizza.Ingredients.Add(ConvertIngredient(pizzaIngredient.Ingredient));
            }
            return convertedPizza;
        }

        private PizzaPriceDto ConverPrice(Price price)
        {
            return new PizzaPriceDto
            {
                Id = price.Id,
                Size = price.Size,
                Thickness = price.DoughThickness,
                Cost = price.Cost,
                Weight = price.Weight
            };
        }

        private PizzaIngredientDto ConvertIngredient(Ingredient ingredient)
        {
            return new PizzaIngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }
    }
}
