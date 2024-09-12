using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _userAccountService.GetAllUser();
            return Ok(user);
        }
        [HttpGet("GetUserById/{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await _userAccountService.GetUserById(Id);
            return Ok(user);
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<UserAccount>> CreateUser([FromForm] CreateUserDTO creUserDTO)
        {
            var createuser = await _userAccountService.CreateUser(creUserDTO);
            return Ok(createuser);
        }
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserDTO updaUserDTO)
        {
            var user = await _userAccountService.UpdateUser(id, updaUserDTO);
            return Ok(user);
        }
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userAccountService.DeleteUser(id);
            return Ok(user);
        }
    }
}
