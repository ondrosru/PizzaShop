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
        private readonly IPizzaIngredientRepository _pizzaIngredientRepository;
        private readonly IRepository<Price> _priceRepository;
        private readonly IRepository<Ingredient> _ingredientRepository;

        public PizzaService(IPizzaRepository pizzaRepository, IPizzaIngredientRepository pizzaIngredientRepository,
            IRepository<Price> priceRepository, IRepository<Ingredient> ingredientRepository)
        {
            _pizzaRepository = pizzaRepository;
            _pizzaIngredientRepository = pizzaIngredientRepository;
            _priceRepository = priceRepository;
            _ingredientRepository = ingredientRepository;
        }

        public PizzaDto GetPizza(int id)
        {
            if (id == 0 )
            {
                PizzaDto newPizza = new PizzaDto();
                newPizza.Prices = new List<PriceDto>();
                newPizza.Ingredients = new List<IngredientDto>();
                newPizza.ImgPath = "";
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

        public PizzaDto SavePizza(PizzaDto newPizza)
        {
            Pizza pizza = _pizzaRepository.GetPizza(newPizza.Id) ?? new Pizza { };
            List<int> oldIngredientIds = new List<int>();
            if (pizza.PizzaIngredients != null)
            {
                foreach (PizzaIngredient ingredient in pizza.PizzaIngredients)
                {
                    oldIngredientIds.Add(ingredient.Ingredient.Id);
                }
            }
            List<int> oldPriceIds = new List<int>();
            if (pizza.Prices != null)
            {
                foreach (Price price in pizza.Prices)
                {
                    oldPriceIds.Add(price.Id);
                }
            }
            pizza.Name = newPizza.Name;
            pizza.Description = newPizza.Description;
            pizza.ImgPath = newPizza.ImgPath;
            pizza = _pizzaRepository.Save(pizza);
            foreach(IngredientDto ingredient in newPizza.Ingredients)
            {
                PizzaIngredient pizzaIngredient = _pizzaIngredientRepository.GetPizzaIngredient(pizza.Id, ingredient.Id) ?? new PizzaIngredient { };
                pizzaIngredient.IngredientId = ingredient.Id;
                pizzaIngredient.PizzaId = pizza.Id;
                pizzaIngredient = _pizzaIngredientRepository.Save(pizzaIngredient);
                Ingredient addedIngredient = new Ingredient
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name
                };
                pizzaIngredient.Ingredient = addedIngredient;
                pizza.PizzaIngredients.Add(pizzaIngredient);
            }
            foreach (PriceDto newPrice in newPizza.Prices)
            {
                Price price = _priceRepository.GetItem(newPrice.Id) ?? new Price { };
                price.PizzaId = pizza.Id;
                price.Size = newPrice.Size;
                price.DoughThickness = newPrice.DoughThickness;
                price.Weight = newPrice.Weight;
                price.Cost = newPrice.Cost;
                price = _priceRepository.Save(price);
                pizza.Prices.Add(price);
            }

            foreach (int oldId in oldIngredientIds)
            {
                List<PizzaIngredient> newIngredinet = pizza.PizzaIngredients.ToList();
                if (newIngredinet.FindIndex(value => value.IngredientId == oldId && value.PizzaId == pizza.Id) == -1)
                {
                    _pizzaIngredientRepository.Delete(oldId);
                }
            }
            foreach (int oldId in oldPriceIds)
            {
                List<Price> newPrices = pizza.Prices.ToList();
                if (newPrices.FindIndex(value => value.Id == oldId) == -1)
                {
                    _priceRepository.Delete(oldId);
                }
            }
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
