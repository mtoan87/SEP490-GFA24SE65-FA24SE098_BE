using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        public DonationService(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }
        public async Task<List<DonationResponseDTO>> GetDonationsByUserIdAsync(string userId)
        {
            return await _donationRepository.GetDonationsByUserId(userId);
        }

        public async Task<List<Village>> GetDonatedVillageByUserId(string userAccountId)
        {
            return await _donationRepository.GetDonatedVillageByUserId(userAccountId);
        }
        public async Task<Donation> CreateDonation(CreateDonationDTO createDonation)
        {
            var donation = new Donation
            {
                UserAccountId = createDonation.UserAccountId,
                DonationType = createDonation.DonationType,
                FacilitiesWalletId = createDonation.FacilitiesWalletId,
                NecessitiesWalletId = createDonation.FacilitiesWalletId,
                SystemWalletId   = createDonation.SystemWalletId,
                HealthWalletId = createDonation.HealthWalletId,
                FoodStuffWalletId = createDonation.FoodStuffWalletId,
                DateTime =DateTime.Now,
                IsDeleted = false,
                Amount = createDonation.Amount,
                Description = createDonation.Description,
                Status = "Pending",
                CreatedDate = DateTime.Now
            };
            await _donationRepository.AddAsync(donation);
            return donation;
        }
        public async Task<Donation> CreateDonationPayment(CreateDonationPayment createDonation)
        {
            var donation = new Donation
            {
                UserAccountId = createDonation.UserAccountId,
                DonationType = createDonation.DonationType,
                DateTime = createDonation.DateTime,
                Amount = createDonation.Amount,
                Description = createDonation.Description,
                IsDeleted = false,
                ChildId = createDonation.ChildId,
                EventId = createDonation.EventId,
                Status = createDonation.Status,
                CreatedDate= DateTime.Now
            };
            await _donationRepository.AddAsync(donation);
            return donation;
        }
        public async Task<Donation> DeleteDonation(int id)
        {
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                throw new Exception($"Donation with ID{id} is not found");
            }
            donation.IsDeleted = true;
            await _donationRepository.UpdateAsync(donation);
            return donation;
        }

        public async Task<IEnumerable<Donation>> GetAllDonations()
        {
            return await _donationRepository.GetAllAsync();
        }

        public async Task<Donation> GetDonationById(int id)
        {
            return await _donationRepository.GetByIdAsync(id);
        }

        public async Task<Donation> RestoreDonation(int id)
        {
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                throw new Exception($"Donation with ID{id} is not found");
            }
            if (donation.IsDeleted == true)
            {
                donation.IsDeleted = false;
                await _donationRepository.UpdateAsync(donation);
            }
            return donation;
        }

        public async Task<Donation> UpdateDonation(int id, UpdateDonationDTO updateDonation)
        {
            var donation = await _donationRepository.GetByIdAsync(id);
            if (donation == null)
            {
                throw new Exception($"Donation with ID{id} is not found");
            }
            donation.UserAccountId = updateDonation.UserAccountId;
            donation.DonationType = updateDonation.DonationType;
            donation.DateTime = updateDonation.DateTime;
            donation.Amount = updateDonation.Amount;
            donation.Description = updateDonation.Description;
            donation.Status = updateDonation.Status;
            donation.ModifiedDate = DateTime.Now;
            await _donationRepository.UpdateAsync(donation);
            return donation;
        }
        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyDonations()
        {
            var donations = await _donationRepository.GetAllAsync();

            // Group donations by year and month
            var monthlyDonations = donations
                .GroupBy(d => new { d.DateTime.Year, d.DateTime.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalAmount = g.Sum(d => d.Amount)
                });

            // Create a nested dictionary to hold the total amounts for each month of each year
            var result = new Dictionary<int, Dictionary<string, decimal>>();
            foreach (var item in monthlyDonations)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, decimal>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");
                result[item.Year][monthName] = item.TotalAmount;
            }

            // Fill in months with zero for years that have no donations
            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = 0.00m; // Set to zero if no donations for that month
                    }
                }
            }

            return result;
        }
        public async Task<int> GetTotalDonationsByYear(int year)
        {
            var donations = await _donationRepository.GetAllAsync();

            // Filter donations for the specified year and count them
            var totalDonations = donations.Count(d => d.DateTime.Year == year);

            return totalDonations;
        }
    }
}
