using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_SERVICE.Interface;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IHouseService _houseService;
        private readonly IUserAccountService _userAccountService;
        private readonly IVillageService _villageService;

        public ExpensesController(IExpenseService expenseService, IHouseService houseService, IUserAccountService userAccountService,IVillageService villageService )
        {
            _expenseService = expenseService;
            _houseService = houseService;   
            _userAccountService = userAccountService;
            _villageService = villageService;
        }
        [HttpGet("FormatedExpenses")]
        public  IActionResult GetFormatedExpenses()
        {
            var exp =  _expenseService.GetFormatedExpenses();
            return Ok(exp);
        }
        [HttpGet("GetUnConfirmHouseExpense")]
        public IActionResult GetUnComfirmHouseExpense()
        {
            var exp = _expenseService.GetUnComfirmHouseExpense();
            return Ok(exp);
        }
        [HttpGet("GetUnComfirmVillageExpense")]
        public IActionResult GetUnComfirmVillageExpense()
        {
            var exp = _expenseService.GetUnComfirmVillageExpense();
            return Ok(exp);
        }
        [HttpGet("ExportExcel")]
        public ActionResult ExportExcel()
        {
            var _expenseData = _expenseService.getExpense();
           
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(_expenseData, "Expense Records");
               
                
                using(MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExpenseRecords.xlsx");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var exp = await _expenseService.GetAllExpenses();
            return Ok(exp);
        }
        [HttpPost("RequestChildExpense")]
        public async Task<IActionResult> RequestChildExpense(RequestSpecialExpenseDTO rq)
        {
            var exp = await _expenseService.RequestChildExpense(rq);
            return Ok(exp);
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchExpenseDTO searchExpenseDTO)
        {
            if (string.IsNullOrEmpty(searchExpenseDTO.SearchTerm))
            {
                return BadRequest("Search term is required.");
            }

            var result = await _expenseService.SearchExpenses(searchExpenseDTO);
            return Ok(result);
        }
        [HttpGet("GetExpenseByFacilitiesWalletId")]
        public IActionResult GetExpenseByFacilitiesWalletId(int Id)
        {
            var exp = _expenseService.GetExpenseByFacilitiesWalletId(Id);
            return Ok(exp);
        }
        [HttpGet("GetExpenseByFoodWalletId")]
        public IActionResult GetExpenseByFoodWalletId(int Id)
        {
            var exp = _expenseService.GetExpenseByFoodWalletId(Id);
            return Ok(exp);
        }
        [HttpGet("GetExpenseByHealthWalletId")]
        public IActionResult GetExpenseByHealthWalletId(int Id)
        {
            var exp = _expenseService.GetExpenseByHealthWalletId(Id);
            return Ok(exp);
        }
        [HttpGet("GetExpenseByNescilitiesWalletId")]
        public IActionResult GetExpenseByNescilitiesWalletId(int Id)
        {
            var exp = _expenseService.GetExpenseByNesceWalletId(Id);
            return Ok(exp);
        }
        [HttpGet("GetExpenseBySystemWalletId")]
        public IActionResult GetExpenseBySystemWalletId(int Id)
        {
            var exp = _expenseService.GetExpenseBySysWalletId(Id);
            return Ok(exp);
        }
        [HttpGet("GetExpenseById/{Id}")]
        public async Task<IActionResult> GetExpenseById(int Id)
        {
            var exp = await _expenseService.GetExpenseById(Id);
            return Ok(exp);
        }
        [HttpPost]
        [Route("CreateExpense")]
        public async Task<ActionResult<Expense>> CreateExoense([FromBody] CreateExepenseDTO expDTO)
        {
            var createExpense = await _expenseService.CreateExpense(expDTO);
            return Ok(createExpense);
        }
        [HttpPut]
        [Route("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] UpdateExpenseDTO updateExp)
        {
            var rs = await _expenseService.UpdateExpense(id, updateExp);
            return Ok(rs);
        }
        [HttpPut]
        [Route("ConfirmExpense")]
        public async Task<IActionResult> UpdateExpense(int id)
        {
            var rs = await _expenseService.ConfirmExpense(id);
            return Ok(rs);
        }
        [HttpPut]
        [Route("ConfirmSpecialExpense")]
        public async Task<IActionResult> ConfirmSpecialExpense(List<string> id,string description,string userName)
        {
            var rs = await _expenseService.ConfirmSpecialExpense(id, description, userName);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var rs = await _expenseService.DeleteExpense(id);
            return Ok(rs);
        }
        [HttpPut]
        [Route("SoftDelete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var rs = await _expenseService.SoftDelete(id);
            return Ok(rs);
        }
    }
}
