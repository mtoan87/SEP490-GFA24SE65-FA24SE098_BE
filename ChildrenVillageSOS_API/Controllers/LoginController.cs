using ChildrenVillageSOS_API.Model;
using ChildrenVillageSOS_DAL.DTO;
using ChildrenVillageSOS_DAL.DTO.AuthDTO;
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
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Model.LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Invalid client request",
                    Data = null,
                    RoleId = 0
                });
            }
            var account = await this._customerService.Login(request.Email, request.Password);
            if (account == null)
            {
                return Unauthorized(new ApiResponse
                {
                    StatusCode = 401,
                    Message = "Unauthorized",
                    Data = null,
                    RoleId = 0
                });
            }

            var token = this.GenerateJSONWebToken(account);
            var user = await _customerService.GetUserById(account.Id);
            var roleId = account.RoleId;
            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = token,
                RoleId = (int)roleId,
                UserId = account.Id
            });
        }

        [HttpPost("LoginGoogle")]
        public async Task<IActionResult> LoginGoogleAsync(LoginGoogleDTO loginGoogleDto)
        {
            var result = await _customerService.LoginWithGoogle(loginGoogleDto.GoogleToken);
            return Ok(result);
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
              new(ClaimTypes.Role, _customerService.GetRoleNameById(userInfo.RoleId)),
              new("userId", userInfo.Id.ToString()),
              },
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
