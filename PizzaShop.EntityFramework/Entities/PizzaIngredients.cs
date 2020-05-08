using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class PizzaIngredient : IEntity
    {
        public int Id { get; set; }
        [Required]
        public int PizzaId { get; set; }
        [Required]
        public int IngredientId { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
