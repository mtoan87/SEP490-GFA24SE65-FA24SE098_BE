using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            var inventories = await _inventoryService.GetAllInventories();
            return Ok(inventories);
        }

        [HttpGet("GetInventoryById/{id}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventoryById(id);
            if (inventory == null)
            {
                return NotFound($"Inventory with ID {id} not found");
            }
            return Ok(inventory);
        }

        [HttpPost("CreateInventory")]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDTO createInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newInventory = await _inventoryService.CreateInventory(createInventory);
            return CreatedAtAction(nameof(GetInventoryById), new { id = newInventory.Id }, newInventory);
        }

        [HttpPut("UpdateInventory/{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] UpdateInventoryDTO updateInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedInventory = await _inventoryService.UpdateInventory(id, updateInventory);
                return Ok(updatedInventory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteInventory/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                var deletedInventory = await _inventoryService.DeleteInventory(id);
                return Ok(deletedInventory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("RestoreInventory/{id}")]
        public async Task<IActionResult> RestoreInventory(int id)
        {
            try
            {
                var restoredInventory = await _inventoryService.RestoreInventory(id);
                return Ok(restoredInventory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
