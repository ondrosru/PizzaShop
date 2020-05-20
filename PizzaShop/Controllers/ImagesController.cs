using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PizzaShop.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IConfiguration _config;
        public ImagesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("GetImage")]
        public IActionResult LoadImage(string name)
        {
            var filePath = _config["Image:Path"] + name;
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, "image/jpeg");
        }
    }
}
