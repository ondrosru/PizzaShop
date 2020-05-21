using PizzaShop.Models;

namespace PizzaShop.Services
{
    public interface IOrderService
    {
        OrderDto SaveOrder(OrderDto newOrder);
    }
}
