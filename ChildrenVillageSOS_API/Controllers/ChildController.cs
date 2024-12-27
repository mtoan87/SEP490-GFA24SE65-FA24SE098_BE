using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildrenController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildren()
        {
            var children = await _childService.GetAllChildren();
            return Ok(children);
        }

        [HttpGet("GetAllChildIsDelete")]
        public async Task<IActionResult> GetAllChildIsDeleteAsync()
        {
            var children = await _childService.GetAllChildIsDeleteAsync();
            return Ok(children);
        }

        [HttpGet("GetAllChildWithImg")]
        public async Task<IActionResult> GetAllChildrenImage()
        {
            var children = await _childService.GetAllChildrenWithImg();
            return Ok(children);
        }

        [HttpGet("GetAllChildWithHealthStatusBad")]
        public async Task<IActionResult> GetAllChildrenBad()
        {
            var children = await _childService.GetAllChildrenWithHealthStatusBad();
            return Ok(children);
        }

        [HttpGet("GetChildWithImg/{Id}")]
        public async Task<IActionResult> GetChildWithImg(string Id)
        {
            var child = await _childService.GetChildByIdWithImg(Id);
            return Ok(child);
        }

        [HttpGet("GetChildById/{Id}")]
        public async Task<IActionResult> GetChildById(string Id)
        {
            var child = await _childService.GetChildById(Id);
            return Ok(child);
        }

        [HttpGet("GetChildDetails/{childId}")]
        public async Task<IActionResult> GetChildDetails(string childId)
        {
            var childDetails = await _childService.GetChildDetails(childId);
            return Ok(childDetails);
        }

        [HttpGet("GetChildByHouseId/{Id}")]
        public async Task<IActionResult> GetChildByHouseIdAsync(string Id)
        {
            var child = await _childService.GetChildByHouseIdAsync(Id);
            return Ok(child);
        }

        [HttpPost]
        [Route("CreateChild")]
        public async Task<ActionResult<Child>> CreateChild([FromForm] CreateChildDTO createChildDTO)
        {
            var newChild = await _childService.CreateChild(createChildDTO);
            return Ok(newChild);
        }

        [HttpPut]
        [Route("UpdateChild")]
        public async Task<IActionResult> UpdateChild(string id, [FromForm] UpdateChildDTO updateChildDTO)
        {
            var updatedChild = await _childService.UpdateChild(id, updateChildDTO);
            return Ok(updatedChild);
        }

        [HttpDelete]
        [Route("DeleteChild")]
        public async Task<IActionResult> DeleteChild(string id)
        {
            var deletedChild = await _childService.DeleteChild(id);
            return Ok(deletedChild);
        }

        [HttpPut("RestoreChild/{id}")]
        public async Task<IActionResult> RestoreChild(string id)
        {
            var restoredChild = await _childService.RestoreChild(id);
            return Ok(restoredChild);
        }
    }
}
