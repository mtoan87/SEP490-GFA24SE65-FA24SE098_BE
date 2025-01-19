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
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ApiResponse
                {
                    StatusCode = 400,
                    Message = "Invalid client request",
                    Data = null
                });
            }

            var user = await _customerService.Login(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized(new ApiResponse
                {
                    StatusCode = 401,
                    Message = "Invalid email or password",
                    Data = null
                });
            }

            var token = _customerService.GenerateToken(user);

            return Ok(new ApiResponse
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = new { Token = token },
                RoleId = user.RoleId,
                UserId = user.Id
            });
        }

        [HttpPost("LoginGoogle")]
        public async Task<IActionResult> LoginGoogleAsync(LoginGoogleDTO loginGoogleDto)
        {
            var result = await _customerService.LoginWithGoogle(loginGoogleDto.GoogleToken);
            return Ok(result);
        }

    }
}
