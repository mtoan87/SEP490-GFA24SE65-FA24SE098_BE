using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Implement;
using ChildrenVillageSOS_SERVICE.Interface;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;
        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDonations()
        {
            var donation = await _donationService.GetAllDonations();
            return Ok(donation);
        }
        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        {
            var _donateData = _donationService.getDonate();
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_donateData, "Donation Records");
                
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DonationRecords.xlsx");
                }
            }
        }
        [HttpGet("FormatDonation")]
        public IActionResult GetAllDonationsArray()
        {
            var donation = _donationService.GetAllDonationArray();
            return Ok(donation);
        }
        [HttpGet("GetDonatedVillageByUserId")]
        public async Task<IActionResult> getDonatedVillageByUserId(string userId)
        {
            var rs = await _donationService.GetDonatedVillageByUserId(userId);
            return Ok(rs);
        }
        [HttpGet("GetDonationByUserId/{Id}")]
        public async Task<IActionResult> GetDonationsByUserIdAsync(string Id)
        {
            var donation = await _donationService.GetDonationsByUserIdAsync(Id);
            return Ok(donation);
        }
        [HttpGet("GetDonationsByUserAndEvent/user/{userId}/event/{eventId}")]
        public async Task<IActionResult> GetDonationsByUserAndEvent(string userId, int eventId)
        {
            var result = await _donationService.GetDonationsByUserAndEventAsync(userId, eventId);

            if (result == null)
                return NotFound("No donations found for the given user and event.");

            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDonationById(int Id)
        {
            var donation = await _donationService.GetDonationById(Id);
            return Ok(donation);
        }

        [HttpPost("CreateDonate")]
        public async Task<ActionResult<Donation>> CreateDonation([FromBody]CreateDonationDTO createDonationDTO)
        {
            var donation = await _donationService.CreateDonation(createDonationDTO);
            return Ok(donation);
        }

        [HttpPut("UpdateDonate")]
        public async Task<IActionResult> UpdateDonation(int Id, UpdateDonationDTO updateDonationDTO)
        {
            var donation = await _donationService.UpdateDonation(Id, updateDonationDTO);
            return Ok(donation);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreDonation(int Id)
        {
            await _donationService.RestoreDonation(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDonation(int Id)
        {
            var donation = await _donationService.DeleteDonation(Id);
            return Ok(donation);
        }
    }
}
