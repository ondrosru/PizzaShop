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
            pizza.Name = newPizza.Name;
            pizza.Description = newPizza.Description;
            pizza.ImgPath = newPizza.ImgPath;
            if (pizza.PizzaIngredients != null)
            {
                for (int i = 0; i < pizza.PizzaIngredients.Count; i++)
                {
                    IngredientDto ingredientData = newPizza.Ingredients.Find(value => value.Id == pizza.PizzaIngredients.ElementAt(i).Id);
                    if (ingredientData == null)
                    {
                        _pizzaIngredientRepository.Delete(pizza.PizzaIngredients.ElementAt(i).Id);
                    }
                    else
                    {
                        PizzaIngredient pizzaIngredient = _pizzaIngredientRepository.GetItem(pizza.PizzaIngredients.ElementAt(i).Id);
                        pizzaIngredient.IngredientId = ingredientData.Id;
                        pizzaIngredient.Ingredient = null;
                        _pizzaIngredientRepository.Save(pizzaIngredient);
                    }
                }
            }
            if (pizza.Prices != null)
            {
                for (int i = 0; i < pizza.Prices.Count; i++)
                {
                    PriceDto priceData = newPizza.Prices.Find(value => value.Id == pizza.Prices.ElementAt(i).Id);
                    if (priceData == null)
                    {
                        _priceRepository.Delete(pizza.Prices.ElementAt(i).Id);
                    }
                    else
                    {
                        Price price = _priceRepository.GetItem(pizza.Prices.ElementAt(i).Id);
                        price.DoughThickness = priceData.DoughThickness;
                        price.Size = priceData.Size;
                        price.Weight = priceData.Weight;
                        price.Cost = priceData.Cost;
                        _priceRepository.Save(price);
                    }
                }
            }
            pizza = _pizzaRepository.Save(pizza);
            foreach (IngredientDto ingredient in newPizza.Ingredients)
            {
                PizzaIngredient pizzaIngredient = null;
                if (pizza.PizzaIngredients != null)
                {
                    pizzaIngredient = pizza.PizzaIngredients.Where(value => value.IngredientId == ingredient.Id).FirstOrDefault();
                }
                if (pizzaIngredient == null)
                {
                    PizzaIngredient newPizzaIngredient = new PizzaIngredient
                    {
                        IngredientId = ingredient.Id,
                        PizzaId = pizza.Id
                    };
                    _pizzaIngredientRepository.Save(newPizzaIngredient);
                }
            }
            foreach (PriceDto price in newPizza.Prices)
            {
                Price oldPrice = _priceRepository.GetItem(price.Id);
                if (oldPrice == null)
                {
                    Price newPrice = new Price
                    {
                        Id = 0,
                        DoughThickness = price.DoughThickness,
                        Size = price.Size,
                        Cost = price.Cost,
                        Weight = price.Weight,
                        PizzaId = pizza.Id,
                    };
                    _priceRepository.Save(newPrice);
                }
            }
            pizza = _pizzaRepository.GetPizza(pizza.Id);
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
