using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using BankAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private IConfiguration _config;

        public LoginController(LoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
        }

        [HttpPost, Route("authenticate")]
        public async Task<IActionResult> Login(AdminDto adminDto)
        {
            var admin = await _loginService.GetAdmin(adminDto);
            if (admin is null) return BadRequest(new { message = "Credenciales invalidas." });

            // generar un token
            string jwtToken = GenerateToken(admin);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(Administrator admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Email, admin.Email),
                new Claim("AdminType", admin.AdminType)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
