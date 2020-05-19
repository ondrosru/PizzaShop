using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        public PizzaController(IPizzaService pizzaService, IConfiguration config)
        {
            _pizzaService = pizzaService;
            _config = config;
        }

        [HttpPost]
        [Route("SavePizza")]
        [Authorize(Policy = Policies.Admin)]
        public PizzaDto SavePizza([FromBody] PizzaDto pizza)
        {
            PizzaDto oldPizza = _pizzaService.GetPizza(pizza.Id);
            if (oldPizza != null)
            {
                if (oldPizza.ImgPath != null && oldPizza.ImgPath != "" && oldPizza.ImgPath != pizza.ImgPath)
                {
                    string fullPath = _config["Image:Path"] + oldPizza.ImgPath;
                    System.IO.File.Delete(fullPath);
                }
            }
            return _pizzaService.SavePizza(pizza);
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
