using ChildrenVillageSOS_DAL.DTO.DonationDetail;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class DonationDetailService : IDonationDetailService
    {
        private readonly IDonationDetailRepository _donationDetailRepository;
        public DonationDetailService(IDonationDetailRepository donationDetailRepository)
        {
            _donationDetailRepository = donationDetailRepository;
        }
        public async Task<DonationDetail> CreateDonationDetail(CreateDonationDetailDTO createDonationDetail)
        {
            var detail = new DonationDetail
            {
                DonationId = createDonationDetail.DonationId,
                Donation = createDonationDetail.Donation,
                Datetime = createDonationDetail.Datetime,
                Description = createDonationDetail.Description,
                VillageId = createDonationDetail.VillageId,
                HouseId = createDonationDetail.HouseId,
                Status = createDonationDetail.Status,
                IsDelete = createDonationDetail.IsDelete,
            };
            await _donationDetailRepository.AddAsync(detail);
            return detail;
        }

        public async Task<DonationDetail> DeleteDonationDetail(int id)
        {
            var detail = await _donationDetailRepository.GetByIdAsync(id);
            if (detail == null)
            {
                throw new Exception($"Donation detail with ID{id} is not found");
            }
            await _donationDetailRepository.RemoveAsync(detail);
            return detail;
        }

        public async Task<IEnumerable<DonationDetail>> GetAllDonationDetails()
        {
            return await _donationDetailRepository.GetAllAsync();
        }

        public async Task<DonationDetail> GetDonationDetailById(int id)
        {
            return await _donationDetailRepository.GetByIdAsync(id);
        }

        public async Task<DonationDetail> UpdateDonationDetail(int id, UpdateDonationDetailDTO updateDonationDetail)
        {
            var detail = await _donationDetailRepository.GetByIdAsync(id);
            if (detail == null)
            {
                throw new Exception($"Donation detail with ID{id} is not found");
            }
            detail.DonationId = updateDonationDetail.DonationId;
            detail.Donation = updateDonationDetail.Donation;
            detail.Datetime = updateDonationDetail.Datetime;
            detail.Description = updateDonationDetail.Description;
            detail.VillageId = updateDonationDetail.VillageId;
            detail.HouseId = updateDonationDetail.HouseId;
            detail.Status = updateDonationDetail.Status;
            detail.IsDelete = updateDonationDetail.IsDelete;

            await _donationDetailRepository.UpdateAsync(detail);
            return detail;
        }
    }
}
