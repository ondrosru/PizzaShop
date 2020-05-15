using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto.Ingredient;
using PizzaShopAdmin.Models;
using PizzaShopAdmin.Services;
using System.Collections.Generic;

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        [Route("GetIngredient")]
        [Authorize(Policy = Policies.Admin)]
        public IngredientDto GetAccount(int id)
        {
            return _ingredientService.GetIngredient(id);
        }

        [HttpGet]
        [Route("GetIngredients")]
        [Authorize(Policy = Policies.Admin)]
        public List<IngredientDto> GetIngredients()
        {
            return _ingredientService.GetIngredients();
        }

        [HttpPost]
        [Route("SaveIngredient")]
        [Authorize(Policy = Policies.Admin)]
        public IngredientDto SaveIngredient([FromBody] IngredientDto ingredient)
        {
            return _ingredientService.SaveIngredient(ingredient);
        }

        [HttpGet]
        [Route("DeleteIngredient")]
        [Authorize(Policy = Policies.Admin)]
        public void SaveIngredient(int id)
        {
            _ingredientService.DeleteIngredient(id);
        }
    }
}
