using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IDonationRepository : IRepositoryGeneric<Donation>
    {
        Task<List<Donation>> GetDonationsByUserIdAsync(string userId);
    }
}
