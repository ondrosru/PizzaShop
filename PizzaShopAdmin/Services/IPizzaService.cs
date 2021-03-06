﻿using PizzaShopAdmin.Dto.Pizza;
using System.Collections.Generic;

namespace PizzaShopAdmin.Services
{
    public interface IPizzaService
    {
        PizzaDto GetPizza(int id);
        List<PizzaDto> GetPizzas();
        PizzaDto SavePizza(PizzaDto newPizza);
        public List<PizzaDto> GetPizzaAtPriceOrderIds(int[] ids);
    }
}
