using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoServer.Models;

namespace ToDoServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }

        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserData data)
        {
            var newUser = new ApplicationUser { Email = data.Email, UserName = data.Email };
            var res = await UserManager.CreateAsync(newUser, data.Password);

            if (res.Succeeded)
            {
                return Ok(GenerateToken(data.Email));
            }
            return StatusCode(500);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserData data)
        {
            var user = await UserManager.FindByEmailAsync(data.Email);
            var valid = await UserManager.CheckPasswordAsync(user, data.Password);

            if (valid)
            {
                return Ok(GenerateToken(data.Email));
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult NewToken([FromBody] UserData data)
        {
            return Ok(GenerateToken(data.Email));
        }

        private string GenerateToken(string email)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddSeconds(20)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT SECRET KEY EXAMPLE")),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class UserData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
