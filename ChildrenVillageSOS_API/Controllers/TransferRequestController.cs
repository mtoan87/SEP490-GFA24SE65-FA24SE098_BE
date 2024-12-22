using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferRequestController : ControllerBase
    {
        private readonly ITransferRequestService _transferRequestService;

        public TransferRequestController(ITransferRequestService transferRequestService)
        {
            _transferRequestService = transferRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferRequest>>> GetAllTransferRequests()
        {
            var transferRequests = await _transferRequestService.GetAllTransferRequests();
            return Ok(transferRequests);
        }

        [HttpGet("GetTransferRequestById/{id}")]
        public async Task<ActionResult<TransferRequest>> GetTransferRequestById(int id)
        {
            var transferRequest = await _transferRequestService.GetTransferRequestById(id);
            if (transferRequest == null)
            {
                return NotFound();
            }
            return Ok(transferRequest);
        }

        [HttpPost("CreateTransferRequest")]
        public async Task<ActionResult<TransferRequest>> CreateTransferRequest(CreateTransferRequestDTO createTransferRequest)
        {
            var transferRequest = await _transferRequestService.CreateTransferRequest(createTransferRequest);
            return CreatedAtAction(nameof(GetTransferRequestById), new { id = transferRequest.Id }, transferRequest);
        }

        [HttpPut("UpdateTransferRequest/{id}")]
        public async Task<IActionResult> UpdateTransferRequest(int id, UpdateTransferRequestDTO updateTransferRequest)
        {
            try
            {
                var updatedTransferRequest = await _transferRequestService.UpdateTransferRequest(id, updateTransferRequest);
                return Ok(updatedTransferRequest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteTransferRequest{id}")]
        public async Task<IActionResult> DeleteTransferRequest(int id)
        {
            try
            {
                await _transferRequestService.DeleteTransferRequest(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("RestoreTransferRequest/{id}")]
        public async Task<IActionResult> RestoreTransferRequest(int id)
        {
            try
            {
                var restoredTransferRequest = await _transferRequestService.RestoreTransferRequest(id);
                return Ok(restoredTransferRequest);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
