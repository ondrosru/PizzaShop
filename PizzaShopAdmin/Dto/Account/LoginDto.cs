using System.ComponentModel.DataAnnotations;

namespace PizzaShopAdmin.Dto.Account
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
