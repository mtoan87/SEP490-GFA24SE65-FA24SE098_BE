using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class DonationRepository : RepositoryGeneric<Donation>, IDonationRepository
    {
        public DonationRepository(SoschildrenVillageDbContext context, ICurrentTime currentTime) : base(context, currentTime)
        {
        }
    }
}
