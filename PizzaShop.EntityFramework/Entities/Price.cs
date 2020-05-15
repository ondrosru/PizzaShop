using PizzaShop.EntityFramework.Entities.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class Price : IEntity
    {
        public int Id { get; set; }
        [Required]
        public PizzaSize Size { get; set; }
        [Required]
        public DoughThickness DoughThickness { get; set; }
        [Required]
        public int PizzaId { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public int Weight { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual ICollection<OrderPrice> OrderPrices { get; set; }
    }
}
