using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaShop.Controllers
{
    [Route("api/[controller]")]
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet("GetPizzas")]
        public List<PizzaDto> GetPizzas()
        {
            return _pizzaService.GetPizzas();
        }

        [HttpGet]
        [Route("GetPizza")]
        public PizzaDto GetPizza(int id)
        {
            return _pizzaService.GetPizza(id);
        }
    }
}
