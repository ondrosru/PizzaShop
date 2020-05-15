using PizzaShopAdmin.Dto.Ingredient;
using System.Collections.Generic;

namespace PizzaShopAdmin.Services
{
    public interface IIngredientService
    {
        IngredientDto GetIngredient(int id);
        List<IngredientDto> GetIngredients();
        void DeleteIngredient(int id);
        IngredientDto SaveIngredient(IngredientDto ingredient);
    }
}
