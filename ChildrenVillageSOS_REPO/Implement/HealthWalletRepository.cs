using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class HealthWalletRepository : RepositoryGeneric<HealthWallet>, IHealthWalletRepository
    {
        public HealthWalletRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
    }
}
