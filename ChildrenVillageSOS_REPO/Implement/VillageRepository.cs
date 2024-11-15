using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class VillageRepository : RepositoryGeneric<Village>, IVillageRepository
    {
        public VillageRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }

        public List<Village> GetVillagesDonatedByUser(string userAccountId)
        {
            using (var dbContext = new SoschildrenVillageDbContext())
            {
                var villageIds = dbContext.Donations
                    .Where(d => d.UserAccountId == userAccountId && (!d.IsDeleted.HasValue || !d.IsDeleted.Value)) // Lọc Donation theo UserAccountId
                    .Join(
                        dbContext.Events, // Tham gia với bảng Events
                        donation => donation.Id, // DonationId từ Donation
                        eventEntity => eventEntity.Id, // EventId từ Event
                        (donation, eventEntity) => eventEntity.VillageId // Lấy VillageId từ Event
                    )
                     // Loại bỏ các giá trị trùng lặp của VillageId
                    .ToList(); // Lấy danh sách các VillageId mà người dùng đã donate

                var villages = dbContext.Villages
                     
                    .ToList(); // Trả về danh sách các làng

                return villages;
            }
        }
    }
}
