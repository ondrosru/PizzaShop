using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class Pizza : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool Status { get; set; }
        public string ImgPath { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
    }
}
