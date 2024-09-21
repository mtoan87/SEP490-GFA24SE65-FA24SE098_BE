using ChildrenVillageSOS_DAL.DTO.SystemWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface ISystemWalletService
    {
        Task<SystemWallet> CreateWallet(CreateSystemWalletDTO createPayment);
        Task<SystemWallet> UpdateWalet(int id, UpdateSystemWalletDTO updatePayment);
        Task<SystemWallet> DeleteWallet(int id);
        Task<SystemWallet> GetWalletById(int id);
        Task<IEnumerable<SystemWallet>> GetSystemWallets();
    }
}
