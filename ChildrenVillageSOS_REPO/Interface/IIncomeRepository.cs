using ChildrenVillageSOS_DAL.DTO.DashboardDTO.KPIStatCards;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IIncomeRepository : IRepositoryGeneric<Income>
    {
        decimal GetTotalIncomeAmount();
        decimal GetMonthlyIncome(int year, int month);
        Task<Income> GetIncomeByDonationIdAsync(int donationId);
        DataTable getIncome();
        IncomeResponseDTO[] GetAllIncome();
        Income[] GetIncomeByFacilitiesWalletId(int id);
        Income[] GetIncomeByFoodWalletId(int id);
        Income[] GetIncomeByHealthWalletId(int id);
        Income[] GetIncomeByNecessilitiesWalletId(int id);
        Income[] GetIncomeBySystemWalletId(int id);
        Task<IEnumerable<Income>> GetIncomesByYear(int year);
    }
}
