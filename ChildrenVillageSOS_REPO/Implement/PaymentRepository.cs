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
    public class PaymentRepository : RepositoryGeneric<Payment>, IPaymentRepository
    {
        public PaymentRepository(SoschildrenVillageDbContext context) : base(context)
        {           
        }
        public async Task<Payment> GetPaymentByDonationIdAsync(int donationId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.DonationId == donationId);
        }
    }
}
