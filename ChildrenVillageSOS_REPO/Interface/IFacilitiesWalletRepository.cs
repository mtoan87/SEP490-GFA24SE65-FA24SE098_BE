using ChildrenVillageSOS_DAL.DTO.FacilitiesWalletDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IFacilitiesWalletRepository : IRepositoryGeneric<FacilitiesWallet>
    {
        Task<FacilitiesWallet> GetFacilitiesWalletByUserIdAsync(string userAccountId);
        FacilitiesWalletResponseDTO[] GetAllToArray();
    }
}
