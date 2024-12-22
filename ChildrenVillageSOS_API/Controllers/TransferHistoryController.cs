using ChildrenVillageSOS_DAL.DTO.TransferHistoryDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferHistoryController : ControllerBase
    {
        private readonly ITransferHistoryService _transferHistoryService;

        public TransferHistoryController(ITransferHistoryService transferHistoryService)
        {
            _transferHistoryService = transferHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferHistory>>> GetAllTransferHistories()
        {
            var transferHistories = await _transferHistoryService.GetAllTransferHistories();
            return Ok(transferHistories);
        }

        [HttpGet("GetTransferHistoryById/{id}")]
        public async Task<ActionResult<TransferHistory>> GetTransferHistoryById(int id)
        {
            var transferHistory = await _transferHistoryService.GetTransferHistoryById(id);
            if (transferHistory == null)
            {
                return NotFound();
            }
            return Ok(transferHistory);
        }

        [HttpPost("CreateTransferHistory")]
        public async Task<ActionResult<TransferHistory>> CreateTransferHistory(CreateTransferHistoryDTO createTransferHistory)
        {
            var transferHistory = await _transferHistoryService.CreateTransferHistory(createTransferHistory);
            return CreatedAtAction(nameof(GetTransferHistoryById), new { id = transferHistory.Id }, transferHistory);
        }


        [HttpPut("UpdateTransferHistory/{id}")]
        public async Task<IActionResult> UpdateTransferHistory(int id, UpdateTransferHistoryDTO updateTransferHistory)
        {
            try
            {
                var updatedTransferHistory = await _transferHistoryService.UpdateTransferHistory(id, updateTransferHistory);
                return Ok(updatedTransferHistory);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/TransferHistory/5
        [HttpDelete("DeleteTransferHistory/{id}")]
        public async Task<IActionResult> DeleteTransferHistory(int id)
        {
            try
            {
                await _transferHistoryService.DeleteTransferHistory(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("RestoreTransferHistory/{id}")]
        public async Task<IActionResult> RestoreTransferHistory(int id)
        {
            try
            {
                var restoredTransferRequest = await _transferHistoryService.RestoreTransferHistory(id);
                return Ok(restoredTransferRequest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
