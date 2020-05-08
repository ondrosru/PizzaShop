using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class Ingredient : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
