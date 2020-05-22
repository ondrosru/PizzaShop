using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShopAdmin.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShopAdmin.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderPrice> _orderPriceRepository;
        public OrderService(IRepository<Order> orderRepository, IRepository<OrderPrice> orderPriceRepository)
        {
            _orderRepository = orderRepository;
            _orderPriceRepository = orderPriceRepository;
        }
        public List<OrderDto> GetOrders()
        {
            return _orderRepository.All.ToList().ConvertAll(ConvertOrder);
        }

        private OrderDto ConvertOrder(Order order)
        {
            OrderDto convertedOrder = new OrderDto
            {
                Id = order.Id,
                AccountId = order.AccountId,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Surname = order.Surname,
                Total = order.Total,
                OrderPriceId = new List<int>()
            };
            if (order.OrderPrices != null)
            {
                order.OrderPrices.ToList().ForEach(orderPrice => convertedOrder.OrderPriceId.Add(orderPrice.Id));
            }
            return convertedOrder;
        }

        public OrderDto GetOrder(int id)
        {
            Order order = _orderRepository.GetItem(id);
            order.OrderPrices = _orderPriceRepository.All.Where(value => value.OrderId == order.Id).ToList();
            return ConvertOrder(order);
        }
    }
}
