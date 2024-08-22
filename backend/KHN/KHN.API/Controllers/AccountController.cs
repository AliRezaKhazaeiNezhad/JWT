using KHN.API.Models;
using KHN.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace KHN.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJWT _jWT;

        public AccountController(IJWT jWT)
        {
            _jWT = jWT;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username != "admin")
            {
                return BadRequest();
            }
            if (model.Password != "123456")
            {
                return BadRequest();
            }


            var tokenString = _jWT.GenerateToken(800, model.Username, 0);

            return Ok(new { token = tokenString });
        }



        [HttpPost]
        public IActionResult ValidateToken([FromBody] string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                bool state = _jWT.ValidateToken(token);

                return Ok();
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("JwtBearer");
            return Ok();
        }



        [Authorize]
        [HttpGet]
        [Route("home")]
        public IActionResult GetProtectedData()
        {
            return Ok();
        }
    }
}
