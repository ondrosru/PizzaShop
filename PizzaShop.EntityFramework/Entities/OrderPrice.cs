using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class OrderPrice : IEntity
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int PriceId { get; set; }

        public virtual Price Price { get; set; }
        public virtual Order Order { get; set; }
    }
}
