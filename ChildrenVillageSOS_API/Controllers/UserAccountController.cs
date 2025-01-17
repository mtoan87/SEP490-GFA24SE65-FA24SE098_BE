using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
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

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchUserDTO searchUserDTO)
        {
            if (string.IsNullOrEmpty(searchUserDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _userAccountService.SearchUserAccounts(searchUserDTO);
            return Ok(result);
        }
        [HttpGet("SearchArray")]
        public async Task<IActionResult> SearchUserArray(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _userAccountService.SearchUserArrayAsync(searchTerm);
            return Ok(result);
        }


        [HttpGet("GetAllUserIsDelete")]
        public async Task<IActionResult> GetAllUserIsDeletedAsync()
        {
            var children = await _userAccountService.GetAllUserIsDeletedAsync();
            return Ok(children);
        }
        [HttpGet("GetAllUserArray")]
        public async Task<IActionResult> GetAllUserArrayAsync()
        {
            var children = await _userAccountService.GetAllUserArrayAsync();
            return Ok(children);
        }

        [HttpGet("GetUserById/{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await _userAccountService.GetUserById(Id);
            return Ok(user);
        }
        [HttpGet("GetUserByIdFormat/{Id}")]
        public async Task<IActionResult> GetUserByIdArray(string Id)
        {
            var user = await _userAccountService.GetUserByIdArray(Id);
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
        [HttpPut]
        [Route("UserUpdate")]
        public async Task<IActionResult> UserUpdate(string id, [FromForm] UserUpdate updaUserDTO)
        {
            var user = await _userAccountService.UserUpdate(id, updaUserDTO);
            return Ok(user);
        }
        [HttpPut("ChangePassword/{Id}")]
        public async Task<IActionResult> ChangePassword(string Id, [FromBody] ChangePassUserDTO changePassUserDTO)
        {
            try
            {
                await _userAccountService.ChangePassword(Id, changePassUserDTO);
                return Ok(new { message = "Password updated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userAccountService.DeleteUser(id);
            return Ok(user);
        }

        [HttpPut]
        [Route("RestoreUser")]
        public async Task<IActionResult> RestoreUser(string id)
        {
            var user = await _userAccountService.RestoreUser(id);
            return Ok(user);
        }

    }
}
