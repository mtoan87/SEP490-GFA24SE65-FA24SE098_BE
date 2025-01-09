using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IDonationService
    {
        Task<DonationResponseDTO[]> GetDonationsByEventAsync(int eventId);
        Task<IEnumerable<Donation>> GetAllDonations();
        Task<Donation> GetDonationById(int id);
        Task<Donation> DonateNow(DonateDTO donateDTO);
        Task<Donation> CreateDonation(CreateDonationDTO createDonation);
        Task<Donation> UpdateDonation(int id, UpdateDonationDTO updateDonation);
        Task<Donation> DeleteDonation(int id);
        Task<Donation> RestoreDonation(int id);
        FormatDonationResponseDTO[] GetAllDonationArray();
        FormatDonationResponseDTO[] GetDonationByEventIdAsync(int eventId);
        FormatDonationResponseDTO[] GetDonationByEventAsync();
        Task<Donation> CreateDonationPayment(CreateDonationPayment createDonation);
        Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyDonations();
        Task<int> GetTotalDonationsByYear(int year);
        Task<List<DonationResponseDTO>> GetDonationsByUserIdAsync(string userId);
        DataTable getDonate();
        Task<List<Village>> GetDonatedVillageByUserId(string userAccountId);
        Task<object> GetDonationsByUserAndEventAsync(string userId, int eventId);
        Task<object> GetDonationsByUserAndChildAsync(string userId, string childId);
        FormatDonationResponseDTO[] GetAllDonationIsDeleteAsync();
        Task<DonationDetailsDTO> GetDonationDetails(int donationId);
        Task<List<Donation>> SearchDonations(SearchDonationDTO searchDonationDTO);
    }
}
