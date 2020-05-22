using PizzaShopAdmin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShopAdmin.Services
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders();
        OrderDto GetOrder(int id);
    }
}
