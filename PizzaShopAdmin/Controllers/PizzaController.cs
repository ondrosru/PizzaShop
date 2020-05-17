using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Dto.Pizza;
using PizzaShopAdmin.Models;
using PizzaShopAdmin.Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PizzaController : Controller
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpPost]
        [Route("SavePizza")]
        [Authorize(Policy = Policies.Admin)]
        public PizzaDto SavePizza([FromBody] PizzaDto pizza)
        {
            return new PizzaDto();
        }

        [HttpGet]
        [Route("GetPizza")]
        [Authorize(Policy = Policies.Admin)]
        public PizzaDto GetPizza(int id)
        {
            return _pizzaService.GetPizza(id);
        }

        [HttpGet]
        [Route("GetPizzas")]
        [Authorize(Policy = Policies.Admin)]
        public List<PizzaDto> GetPizzas()
        {
            return _pizzaService.GetPizzas();
        }
    }
}
