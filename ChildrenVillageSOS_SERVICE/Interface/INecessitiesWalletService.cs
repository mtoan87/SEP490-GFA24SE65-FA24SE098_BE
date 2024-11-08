using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface INecessitiesWalletService
    {
        Task<NecessitiesWallet> DeleteNecessitiesWallet(int id);
        Task<NecessitiesWallet> UpdateNecessitiesWalet(int id, UpdateHealthWalletDTO updatePayment);
        Task<NecessitiesWallet> CreateNecessitiesWallet(CreateHealthWalletDTO createPayment);
        Task<decimal> GetTotalBudget();
        Task<NecessitiesWallet> GetNecessitiesWalletById(int id);
        Task<IEnumerable<NecessitiesWallet>> GetNecessitiesWallets();
    }
}
