using PizzaShop.EntityFramework.Entities.Enum;

namespace PizzaShopAdmin.Dto.Pizza
{
    public class PriceDto
    {
        public int Id { get; set; }
        public PizzaSize Size { get; set; }
        public DoughThickness DoughThickness { get; set; }
        public float Cost { get; set; }
        public int Weight { get; set; }
    }
}
