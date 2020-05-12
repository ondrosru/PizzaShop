using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PizzaShopAdmin.Dto.Account;
using PizzaShopAdmin.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PizzaShopAdmin.Models;

namespace PizzaShopAdmin.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        public LoginController(IConfiguration configuration, IAccountService accountService)
        {
            _config = configuration;
            _accountService = accountService;
        }

        [HttpPost("SingIn")]
        [AllowAnonymous]
        public IActionResult SingIn([FromBody]LoginDto login)
        {
            IActionResult response = Unauthorized();
            AccountDto account = _accountService.GetAccount(login);

            if (account != null && account.Role == Policies.Admin)
            {
                var tokenString = GenerateJWT(account);
                response = Ok(new
                {
                    token = tokenString
                });
            }
            return response;
        }

        private string GenerateJWT(AccountDto accountInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials= new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountInfo.Id.ToString()),
                new Claim("role", accountInfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
               );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
