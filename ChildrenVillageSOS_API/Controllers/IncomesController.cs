using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IHouseService _houseService;
        private readonly IUserAccountService _userAccountService;
        private readonly IVillageService _villageService;
        public IncomesController(IIncomeService incomeService,IHouseService houseService, IUserAccountService userAccountService, IVillageService villageService )
        {
            _incomeService = incomeService;
            _houseService = houseService;
            _userAccountService = userAccountService;
            _villageService = villageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var exp = await _incomeService.GetAllIncomes();
            return Ok(exp);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchIncomeDTO searchIncomeDTO)
        {
            if (string.IsNullOrEmpty(searchIncomeDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _incomeService.SearchIncomes(searchIncomeDTO);
            return Ok(result);
        }
        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        {
            var _incomeData = _incomeService.getIncome();
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_incomeData, "Income Records");
                
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "IncomeRecords.xlsx");
                }
            }
        }
        [HttpGet("FormatedIncome")]
        public  IActionResult GetFormatedIncome()
        {
            var exp =  _incomeService.GetFormatedIncome();
            return Ok(exp);
        }
        [HttpGet("GetIncomeByFacilitiesWallet")]
        public IActionResult GetIncomeByFacilitiesWalletId(int Id)
        {
            var exp = _incomeService.GetIcomeByFaciWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByFoodWallet")]
        public IActionResult GetIncomeByFoodWalletId(int Id)
        {
            var exp = _incomeService.GetIcomeByFoodWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByHealthWallet")]
        public IActionResult GetIncomeByHealthWallet(int Id)
        {
            var exp = _incomeService.GetIcomeByHealthWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeByNescilitiesWallet")]
        public IActionResult GetIncomeByNescilitiesWallet(int Id)
        {
            var exp = _incomeService.GetIcomeByNesWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeBySystemWallet")]
        public IActionResult GetIncomeBySystemWallet(int Id)
        {
            var exp = _incomeService.GetIcomeBySystemWallet(Id);
            return Ok(exp);
        }
        [HttpGet("GetIncomeById/{Id}")]
        public async Task<IActionResult> GetIncomeById(int Id)
        {
            var exp = await _incomeService.GetIncomeById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateIncome")]
        public async Task<ActionResult<Income>> CreateIncome([FromBody] CreateIncomeDTO expDTO)
        {
            var createExpense = await _incomeService.CreateIncome(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateIncome")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] UpdateIncomeDTO updateExp)
        {
            var rs = await _incomeService.UpdateIncome(id, updateExp);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteIncome")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var rs = await _incomeService.DeleteIncome(id);
            return Ok(rs);
        }
        [HttpPut]
        [Route("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var rs = await _incomeService.SoftDelete(id);
            return Ok(rs);
        }
    }
}
