using ChildrenVillageSOS_DAL.DTO.TransferRequestDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetAllTransferRequests")]
        public async Task<ActionResult<IEnumerable<TransferRequest>>> GetAllTransferRequests()
        {
            var results = await _transferRequestService.GetAllTransferRequests();
            return Ok(results);
        }

        [HttpGet("GetTransferRequestById/{id}")]
        public async Task<ActionResult<TransferRequest>> GetTransferRequestById(int id)
        {
            var result = await _transferRequestService.GetTransferRequestById(id);
            return Ok(result);
        }

        [HttpGet("GetTransferRequestsByHouse/{houseId}")]
        public async Task<ActionResult<IEnumerable<TransferRequest>>> GetTransferRequestsByHouse(string houseId)
        {
            var results = await _transferRequestService.GetTransferRequestsByHouse(houseId);
            return Ok(results);
        }

        [HttpPost("CreateTransferRequest")]
        public async Task<ActionResult<TransferRequest>> CreateTransferRequest([FromForm] CreateTransferRequestDTO dto)
        {
            var result = await _transferRequestService.CreateTransferRequest(dto);
            return Ok(result);
        }

        [HttpPut("UpdateTransferRequest/{id}")]
        public async Task<ActionResult<TransferRequest>> UpdateTransferRequest(int id, [FromForm] UpdateTransferRequestDTO dto)
        {
            var result = await _transferRequestService.UpdateTransferRequest(id, dto);
            return Ok(result);
        }     
    }
}
