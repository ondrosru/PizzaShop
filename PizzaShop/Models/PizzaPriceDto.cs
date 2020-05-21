using PizzaShop.EntityFramework.Entities.Enum;

namespace PizzaShop.Models
{
    public class PizzaPriceDto
    {
        public int Id { get; set; }
        public PizzaSize Size { get; set; }
        public DoughThickness Thickness { get; set; }
        public float Cost { get; set; }
        public int Weight { get; set; }
        public int? Count { get; set; }
    }
}
