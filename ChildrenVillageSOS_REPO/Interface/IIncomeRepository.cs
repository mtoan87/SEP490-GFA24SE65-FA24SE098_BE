using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IIncomeRepository : IRepositoryGeneric<Income>
    {
        Task<Income> GetIncomeByDonationIdAsync(int donationId);

        IncomeResponseDTO[] GetAllIncome();
    }
}
