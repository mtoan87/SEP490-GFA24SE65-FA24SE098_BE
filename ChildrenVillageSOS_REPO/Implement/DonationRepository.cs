using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Helper;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class DonationRepository : RepositoryGeneric<Donation>, IDonationRepository
    {
        public DonationRepository(SoschildrenVillageDbContext context) : base(context)
        {
        }
        public async Task<List<Donation>> GetDonationsByEventIdAsync(int eventId)
        {
            return await _context.Donations
                .Include(d => d.Event) // Ensure Event is loaded
                .Where(d => d.EventId == eventId && !d.IsDeleted)
                .ToListAsync();
        }
        public DataTable getDonate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "DonationData";
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("DateTime", typeof(DateTime));

            // Truy vấn dữ liệu từ bảng Donation
            var donations = this._context.Donations.ToList();

            if (donations.Count > 0)
            {
                donations.ForEach(donation =>
                {
                    dt.Rows.Add(
                        donation.UserName,
                        donation.Amount,
                        donation.DateTime
                    );
                });
            }

            return dt;
        }
        public async Task<List<Donation>> GetDonationsByUserIdAsync(string userId)
        {
            return await _context.Donations
                .Where(d => d.UserAccountId == userId && (d.IsDeleted == null || d.IsDeleted == false))                
                .ToListAsync();
        }
        public FormatDonationResponseDTO[] GetDonationArray()
        {
            var donationDetails = _context.Donations
                .Where(d => !d.IsDeleted)
                .Select(d => new FormatDonationResponseDTO
            {
                Id = d.Id,
                UserAccountId = d.UserAccountId,
                UserName = d.UserName,
                UserEmail = d.UserEmail,
                Phone = d.Phone,
                Address = d.Address,
                DonationType = d.DonationType,
                DateTime = d.DateTime,
                Amount = d.Amount,
                Description = d.Description,
                SystemWalletId = d.SystemWalletId,
                FacilitiesWalletId = d.FacilitiesWalletId,
                FoodStuffWalletId = d.FoodStuffWalletId,
                HealthWalletId = d.HealthWalletId,
                NecessitiesWalletId = d.NecessitiesWalletId,
                ChildId = d.ChildId,
                EventId = d.EventId,
                Status = d.Status,
            }).ToArray();
            return donationDetails;
        }
        public Task<List<DonationResponseDTO>> GetDonationsByUserId(string userId)
        {
            var donationDetails = _context.Donations
                .Where(d => d.UserAccountId == userId && (d.IsDeleted == null || d.IsDeleted == false))
                .Select (d => new DonationResponseDTO
                {
                    Id = d.Id,
                    UserAccountId = d.UserAccountId,
                    DonationType = d.DonationType,
                    DateTime = d.DateTime,
                    Amount = d.Amount,
                    Description = d.Description,
                    Status = d.Status,
                    CreatedDate = d.CreatedDate,
                }).ToListAsync();
            return donationDetails;
        }

        public async Task<List<Donation>> GetDonationsByUserAndEventAsync(string userId, int eventId)
        {
            return await _context.Donations
                .Where(d => d.UserAccountId == userId && d.EventId == eventId && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Donation>> GetDonationsByUserAndChildAsync(string userId, string childId)
        {
            return await _context.Donations
                .Where(d => d.UserAccountId == userId && d.ChildId == childId && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Village>> GetDonatedVillageByUserId(string userAccountId)
        {
            return await _context.Donations
                .Where(d => d.UserAccountId == userAccountId && d.IsDeleted != true)
                .Include(d => d.Event)
                .ThenInclude(e => e.Village)
                .Where(d => d.Event != null && d.Event.Village != null)
                .Select(d => d.Event.Village)
                .Distinct()
                .ToListAsync();
        }

        public async Task<DonationTrendsDTO> GetDonationTrendsByYear(int year)
        {
            var monthlyTrends = await _context.Donations
                .Where(d => d.DateTime.Year == year && !d.IsDeleted)
                .GroupBy(d => new {
                    Month = d.DateTime.Month,
                    DonationType = d.DonationType
                })
                .Select(g => new {
                    Month = g.Key.Month,
                    DonationType = g.Key.DonationType,
                    Amount = g.Sum(d => d.Amount)
                })
                .ToListAsync();

            // Khởi tạo chi tiết cho 12 tháng
            var monthlyDetails = Enumerable.Range(1, 12)
                .Select(month => new MonthlyDonationDetail
                {
                    Month = month,
                    EventAmount = 0,
                    ChildAmount = 0,
                    WalletAmount = 0,
                    TotalAmount = 0
                })
                .ToList();

            // Điền dữ liệu từ query
            foreach (var trend in monthlyTrends)
            {
                var monthDetail = monthlyDetails.First(m => m.Month == trend.Month);

                switch (trend.DonationType)
                {
                    case "Event":
                        monthDetail.EventAmount = trend.Amount;
                        break;
                    case "Child":
                        monthDetail.ChildAmount = trend.Amount;
                        break;
                    case "Wallet":
                        monthDetail.WalletAmount = trend.Amount;
                        break;
                }

                monthDetail.TotalAmount += trend.Amount;
            }

            return new DonationTrendsDTO
            {
                Year = year,
                MonthlyDetails = monthlyDetails
            };
        }
    }
}
