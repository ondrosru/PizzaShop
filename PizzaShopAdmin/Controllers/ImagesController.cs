using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaShopAdmin.Models;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        public ImagesController()
        {
        }

        [HttpPost]
        [Route("SaveImage")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult SaveImage()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                string newPath = "D:\\Pictures\\PizzaShop";
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fileName = "";
                if (file.Length > 0)
                {
                    fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetImage")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult LoadImage(string name)
        {
            var filePath = "D:\\Pictures\\PizzaShop\\" + name;
            byte[] b = System.IO.File.ReadAllBytes(filePath);
            return File(b, "image/jpeg");
        }
    }
}
