using ChildrenVillageSOS_DAL.DTO.FacilitiesWalletDTO;
using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IFacilitiesWalletService
    {
        Task<IEnumerable<FacilitiesWallet>> GetFacilitiesWallets();
        Task<FacilitiesWallet> GetFacilitiesWalletById(int id);
        Task<FacilitiesWallet> CreateFacilitiesWallet(CreateFacilitiesWalletDTO createPayment);
        Task<FacilitiesWallet> UpdateFacilitiesWalet(int id, UpdateFacilitiesWalletDTO updatePayment);
        Task<FacilitiesWallet> DeleteFacilitiesWallet(int id);
        Task<decimal> GetTotalBudget();
    }
}
