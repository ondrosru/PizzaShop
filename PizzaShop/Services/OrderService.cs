using PizzaShop.EntityFramework.Entities;
using PizzaShop.EntityFramework.Repositories;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShop.Services
{
    public class OrderService: IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderPrice> _orderPriceRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<OrderPrice> orderPriceRepository)
        {
            _orderRepository = orderRepository;
            _orderPriceRepository = orderPriceRepository;
        }

        public OrderDto SaveOrder(OrderDto newOrder)
        {
            Order order = _orderRepository.GetItem(newOrder.Id) ?? new Order { };
            order.Name = newOrder.Name;
            order.Surname = newOrder.Surname;
            order.Phone = newOrder.Phone;
            order.Total = newOrder.Total;
            if (newOrder.AccountId == 0 || newOrder.AccountId == null)
            {
                order.AccountId = null;
            }
            else
            {
                order.AccountId = newOrder.AccountId;
            }
            order.Address = newOrder.Address;
            if (order.Id == 0)
            {
                order.CreatedAt = DateTime.Now;
            }
            order = _orderRepository.Save(order);
            if (order.OrderPrices != null)
            {
                order.OrderPrices.ToList().ForEach(price =>
                {
                    PizzaPriceDto foundPrice = null;
                    newOrder.Pizzas.ForEach(pizza =>
                    {
                        foundPrice = pizza.Prices.Find(newPrice =>
                        {
                            return newPrice.Id == price.PriceId;
                        });
                    });
                    if (foundPrice == null)
                    {
                        _orderPriceRepository.Delete(price.Id);
                    }
                });
            }
            newOrder.Pizzas.ForEach(pizza =>
            {
                pizza.Prices.ForEach(price =>
                {
                    OrderPrice foundOrderPrice = null;
                    if (order.OrderPrices != null)
                    {
                        foundOrderPrice = order.OrderPrices.ToList().Find(orderPrice => orderPrice.PriceId == price.Id);
                    }
                    if (foundOrderPrice == null)
                    {
                        OrderPrice newOrderPrice = new OrderPrice {
                            Id = 0,
                            Count = (int)price.Count,
                            OrderId = order.Id,
                            PriceId = price.Id
                        };
                        _orderPriceRepository.Save(newOrderPrice);
                    }
                });
            });
            return ConvertOrder(order);
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
                Total = order.Total
            };
            convertedOrder.Pizzas = new List<PizzaDto>();
            return convertedOrder;
        }
    }
}
