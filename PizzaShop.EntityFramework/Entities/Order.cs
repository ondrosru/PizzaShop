using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<OrderPrice> OrderPrices { get; set; }
        public virtual Account Account { get; set; }
    }
}
