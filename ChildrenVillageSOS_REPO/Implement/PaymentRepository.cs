using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
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

        //dashboard
        public async Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatistics()
        {
            var statistics = await _context.Payments
                .Where(p => p.IsDeleted != true && p.Status == "Paid")
                .GroupBy(p => p.PaymentMethod)
                .Select(g => new PaymentMethodStatsDTO
                {
                    PaymentMethod = g.Key,
                    NumberOfUses = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount)
                })
                .ToListAsync();

            return statistics;
        }

        //Dự phòng nếu cần
        //public async Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatisticsByDateRange(DateTime startDate, DateTime endDate)
        //{
        //    var statistics = await _context.Payments
        //        .Where(p => p.IsDeleted != true
        //                && p.Status == "Paid"
        //                && p.DateTime >= startDate
        //                && p.DateTime <= endDate)
        //        .GroupBy(p => p.PaymentMethod)
        //        .Select(g => new PaymentMethodStatsDTO
        //        {
        //            PaymentMethod = g.Key,
        //            NumberOfUses = g.Count(),
        //            TotalAmount = g.Sum(p => p.Amount)
        //        })
        //        .ToListAsync();

        //    return statistics;
        //}
    }
}
