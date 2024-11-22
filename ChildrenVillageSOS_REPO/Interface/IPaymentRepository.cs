using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IPaymentRepository : IRepositoryGeneric<Payment>
    {
        Task<Payment> GetPaymentByDonationIdAsync(int donationId);
        //dashboard
        Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatistics();
        //Task<IEnumerable<PaymentMethodStatsDTO>> GetPaymentMethodStatisticsByDateRange(DateTime startDate, DateTime endDate);
    }
}
