using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("GetAccount")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAccountData()
        {
            return Ok("This is user");
        }
    }
}
