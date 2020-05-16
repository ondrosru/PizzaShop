using PizzaShop.EntityFramework.Entities;
using PizzaShopAdmin.Dto.Ingredient;
using System.Collections.Generic;

namespace PizzaShopAdmin.Dto.Pizza
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public List<PriceDto> Prices { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
