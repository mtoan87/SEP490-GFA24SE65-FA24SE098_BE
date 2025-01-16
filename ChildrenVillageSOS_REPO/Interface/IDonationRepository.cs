using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IDonationRepository : IRepositoryGeneric<Donation>
    {
        Task<List<Donation>> GetDonationsByEventIdAsync(int eventId);
        decimal GetTotalDonateAmount();
        DataTable getDonate();
        Task<List<Donation>> GetDonationsByUserIdAsync(string userId);
        FormatDonationResponseDTO[] GetDonationByUserIdFormat(string userId);
        Task<List<DonationResponseDTO>> GetDonationsByUserId(string userId);
        Task<List<Village>> GetDonatedVillageByUserId(string userAccountId);
        FormatDonationResponseDTO[] GetDonationArray();
        FormatDonationResponseDTO[] GetDonationByEventIdArray(int eventId);
        FormatDonationResponseDTO[] GetDonationByEventArray();
        Task<List<Donation>> GetDonationsByUserAndEventAsync(string userId, int eventId);
        Task<List<Donation>> GetDonationsByUserAndChildAsync(string userId, string childId);
        Task<DonationTrendsDTO> GetDonationTrendsByYear(int year);
        FormatDonationResponseDTO[] GetDonationIsDeleteArray();
        Task<DonationDetailsDTO> GetDonationDetails(int donationId);
        decimal GetTotalDonationThisMonth();
        Task<List<Donation>> SearchDonations(SearchDonationDTO searchDonationDTO);

    }
}
