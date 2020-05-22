using System.Collections.Generic;

namespace PizzaShopAdmin.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public float Total { get; set; }
        public List<int> OrderPriceId { get; set; }
    }
}
