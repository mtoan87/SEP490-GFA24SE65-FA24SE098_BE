using ChildrenVillageSOS_DAL.DTO.FoodWalletDTO;
using ChildrenVillageSOS_DAL.DTO.HealthWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IHealthWalletService
    {
        Task<IEnumerable<HealthWallet>> GetHealthWallets();
        Task<HealthWallet> GetHealthWalletById(int id);
        Task<HealthWallet> CreateHealthWallet(CreateHealthWalletDTO createPayment);
        Task<HealthWallet> UpdateHealthWalet(int id, UpdateHealthWalletDTO updatePayment);
        Task<HealthWallet> DeleteHealthWallet(int id);
    }
}
