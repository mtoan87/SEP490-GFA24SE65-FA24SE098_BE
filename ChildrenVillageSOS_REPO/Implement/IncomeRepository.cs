using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class IncomeRepository : RepositoryGeneric<Income> ,IIncomeRepository
    {
        public IncomeRepository(SoschildrenVillageDbContext context) : base(context)
        {
            
        }
        public async Task<Income> GetIncomeByDonationIdAsync(int donationId)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(p => p.DonationId == donationId);
        }
    }
}
