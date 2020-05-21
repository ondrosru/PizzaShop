using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController (IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("SaveOrder")]
        public OrderDto SaveOrder([FromBody] OrderDto order)
        {
            return _orderService.SaveOrder(order);
        }
    }
}
