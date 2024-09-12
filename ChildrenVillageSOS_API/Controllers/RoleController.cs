using ChildrenVillageSOS_DAL.DTO.RoleDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            var rol = await _roleService.GetAllRole();
            return Ok(rol);
        }
        [HttpGet("GetRoleById/{Id}")]
        public async Task<IActionResult> GetRoleById(int Id)
        {
            var rol = await _roleService.GetRoleById(Id);
            return Ok(rol);
        }
        [HttpPost]
        [Route("CreateRole")]
        public async Task<ActionResult<Role>> CreateRole([FromForm] CreateRoleDTO rolDTO)
        {
            var rol = await _roleService.CreateRole(rolDTO);
            return Ok(rol);
        }
        [HttpPut]
        [Route("UpdateRole")]
        public async Task<IActionResult> UpdateRole(int id, [FromForm] UpdateRoleDTO rolDTO)
        {
            var rol = await _roleService.UpdateRole(id, rolDTO);
            return Ok(rol);
        }
        [HttpDelete]
        [Route("DeleteRole")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var rol = await _roleService.DeleteRole(id);
            return Ok(rol); 
        }
    }
}
