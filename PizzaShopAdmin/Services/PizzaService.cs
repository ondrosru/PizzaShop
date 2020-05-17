using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto.Ingredient;
using PizzaShopAdmin.Dto.Pizza;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopAdmin.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public PizzaDto GetPizza(int id)
        {
            if (id == 0 )
            {
                PizzaDto newPizza = new PizzaDto();
                newPizza.Prices = new List<PriceDto>();
                newPizza.Ingredients = new List<IngredientDto>();
                return newPizza;
            }
            Pizza pizza = _pizzaRepository.GetPizza(id);
            return ConvertPizza(pizza);
        }

        public List<PizzaDto> GetPizzas()
        {
            List<Pizza> pizzas = _pizzaRepository.GetPizzas().ToList();
            return pizzas.ConvertAll(ConvertPizza);
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
            convertedPizza.Ingredients = new List<IngredientDto>();
            foreach (PizzaIngredient pizzaIngredient in pizza.PizzaIngredients)
            {
                convertedPizza.Ingredients.Add(ConvertIngredient(pizzaIngredient.Ingredient));
            }
            return convertedPizza;
        }

        private PriceDto ConverPrice(Price price)
        {
            return new PriceDto
            {
                Id = price.Id,
                Size = price.Size,
                DoughThickness = price.DoughThickness,
                Cost = price.Cost,
                Weight = price.Weight
            };
        }

        private IngredientDto ConvertIngredient(Ingredient ingredient)
        {
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }
    }
}
