using System.Collections.Generic;

namespace PizzaShop.Models
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public List<PizzaPriceDto> Prices { get; set; }
        public List<PizzaIngredientDto> Ingredients { get; set; }
    }
}
