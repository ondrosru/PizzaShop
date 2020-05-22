using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto;
using PizzaShopAdmin.Models;
using PizzaShopAdmin.Services;

namespace PizzaShopAdmin.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("GetOrders")]
        [Authorize(Policy = Policies.Admin)]
        public List<OrderDto> GetOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpGet]
        [Route("GetOrder")]
        [Authorize(Policy = Policies.Admin)]
        public OrderDto GetOrder(int id)
        {
            return _orderService.GetOrder(id);
        }
    }
}
