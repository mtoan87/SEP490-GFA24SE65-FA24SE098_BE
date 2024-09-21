using ChildrenVillageSOS_API.Model;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserAccountService _customerService;
        private readonly IConfiguration _config;
        public LoginController(IConfiguration configuration, IUserAccountService customerService)
        {
            _config = configuration;
            _customerService = customerService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Model.LoginRequest request)
        {
            var account = await this._customerService.Login(request.UserEmail, request.Password);
            if (account == null)
                return Unauthorized();

            var token = this.GenerateJSONWebToken(account);
            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              new Claim[]
              {
              new(ClaimTypes.Email, userInfo.UserEmail),
              new(ClaimTypes.Role, userInfo.RoleId.ToString()),
              new("userId", userInfo.Id.ToString()),
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
