using ChildrenVillageSOS_DAL.DTO.BookingDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllDonations();
        Task<Donation> GetDonationById(int id);
        Task<Donation> CreateDonation(CreateDonationDTO createDonation);
        Task<Donation> UpdateDonation(int id, UpdateDonationDTO updateDonation);
        Task<Donation> DeleteDonation(int id);
        Task<Donation> RestoreDonation(int id);
        Task<Donation> CreateDonationPayment(CreateDonationPayment createDonation);
        Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyDonations();
        Task<int> GetTotalDonationsByYear(int year);
    }
}
