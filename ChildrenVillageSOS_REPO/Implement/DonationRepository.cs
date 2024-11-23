using ChildrenVillageSOS_DAL.DTO.DonationDTO;
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
    public class DonationRepository : RepositoryGeneric<Donation>, IDonationRepository
    {
        public DonationRepository(SoschildrenVillageDbContext context) : base(context)
        {
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
    }
}
