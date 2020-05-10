using PizzaShop.EntityFramework.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.EntityFramework.Entities
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
