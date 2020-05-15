using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto.Ingredient;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopAdmin.Services
{
    public class IngredientSerivce : IIngredientService
    {
        private readonly IRepository<Ingredient> _ingredientRepository;
        public IngredientSerivce(IRepository<Ingredient> ingredientRepositroy)
        {
            _ingredientRepository = ingredientRepositroy;
        }

        public void DeleteIngredient(int id)
        {
            _ingredientRepository.Delete(id);
        }

        public IngredientDto GetIngredient(int id)
        {
            return Convert(_ingredientRepository.GetItem(id));
        }

        public List<IngredientDto> GetIngredients()
        {
            return _ingredientRepository.All.ToList().ConvertAll(Convert);
        }

        public IngredientDto SaveIngredient(IngredientDto newIngredient)
        {
            Ingredient ingredient = _ingredientRepository.GetItem(newIngredient.Id) ?? new Ingredient { };
            ingredient.Name = newIngredient.Name;
            ingredient = _ingredientRepository.Save(ingredient);
            return Convert(ingredient);
        }

        private IngredientDto Convert(Ingredient ingredient)
        {
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }
    }
}
