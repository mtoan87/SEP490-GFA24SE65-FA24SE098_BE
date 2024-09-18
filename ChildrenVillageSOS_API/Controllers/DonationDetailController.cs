//using ChildrenVillageSOS_DAL.DTO.DonationDetail;
//using ChildrenVillageSOS_DAL.DTO.DonationDTO;
//using ChildrenVillageSOS_DAL.Models;
//using ChildrenVillageSOS_SERVICE.Implement;
//using ChildrenVillageSOS_SERVICE.Interface;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ChildrenVillageSOS_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class DonationDetailController : ControllerBase
//    {
//        private readonly IDonationDetailService _donationDetailService;
//        public DonationDetailController(IDonationDetailService donationDetailService)
//        {
//            _donationDetailService = donationDetailService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllDonationDetails()
//        {
//            var detail = await _donationDetailService.GetAllDonationDetails();
//            return Ok(detail);
//        }

//        [HttpGet("{Id}")]
//        public async Task<IActionResult> GetDonationDetailById(int Id)
//        {
//            var detail = await _donationDetailService.GetDonationDetailById(Id);
//            return Ok(detail);
//        }

//        [HttpPost]
//        public async Task<ActionResult<DonationDetail>> CreateDonationDetail(CreateDonationDetailDTO createDonationDetailDTO)
//        {
//            var detail = await _donationDetailService.CreateDonationDetail(createDonationDetailDTO);
//            return Ok(detail);
//        }

//        [HttpPut("{Id}")]
//        public async Task<IActionResult> UpdateDonationDetail(int Id, UpdateDonationDetailDTO updateDonationDetailDTO)
//        {
//            var detail = await _donationDetailService.UpdateDonationDetail(Id, updateDonationDetailDTO);
//            return Ok(detail);
//        }

//        [HttpDelete("{Id}")]
//        public async Task<IActionResult> DeleteDonationDelete(int Id)
//        {
//            var detail = await _donationDetailService.DeleteDonationDetail(Id);
//            return Ok(detail);
//        }
//    }
//}
