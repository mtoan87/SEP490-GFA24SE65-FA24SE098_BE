using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IIncomeService
    {
        DataTable getIncome();
        Task<IEnumerable<Income>> GetAllIncomes();
        IncomeResponseDTO[] GetFormatedIncome();
        Task<Income> GetIncomeById(int id);
        Task<Income> GetIncomeByDonationIdAsync(int donationId);
        Task<Income> CreateIncome(CreateIncomeDTO createIncome);
        Task<Income> UpdateIncome(int id, UpdateIncomeDTO updateIncome);
        Task<Income> DeleteIncome(int id);
        Task<Income> SoftDelete(int id);
        Income[] GetIcomeByFaciWallet(int id);
        Income[] GetIcomeByFoodWallet(int id);
        Income[] GetIcomeByHealthWallet(int id);
        Income[] GetIcomeByNesWallet(int id);
        Income[] GetIcomeBySystemWallet(int id);
        Task<List<Income>> SearchIncomes(SearchIncomeDTO searchIncomeDTO);
    }
}
