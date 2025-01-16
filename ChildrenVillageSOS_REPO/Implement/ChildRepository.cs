using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.Charts;
using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.HealthReportDTO;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.DTO.SubjectDetailDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class ChildRepository : RepositoryGeneric<Child>, IChildRepository
    {
        public ChildRepository(SoschildrenVillageDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Child>> GetAllNotDeletedAsync()
        {
            // Sử dụng Include để lấy các hình ảnh liên quan đến Event
            return await _context.Children
                                 .Include(e => e.Images)  // Dùng Include để lấy các hình ảnh liên quan
                                 .Where(e => !e.IsDeleted) // Nếu cần, lọc các sự kiện không bị xóa
                                 .ToListAsync();
        }
        public async Task<ChildResponseDTO[]> GetChildByHouseId(string houseId)
        {
            return await _context.Children
                .Where(x => x.HouseId == houseId && !x.IsDeleted) // Ensure the event is not deleted
                .Select(x => new ChildResponseDTO
                {
                    Id = x.Id,
                    ChildName = x.ChildName,
                    HealthStatus = x.HealthStatus,
                    HouseId = x.HouseId,
                    SchoolId = x.SchoolId,
                    FacilitiesWalletId = x.FacilitiesWalletId,
                    SystemWalletId = x.SystemWalletId,
                    FoodStuffWalletId = x.FoodStuffWalletId,
                    HealthWalletId = x.HealthWalletId,
                    NecessitiesWalletId = x.NecessitiesWalletId,
                    Amount = x.Amount ?? 0,
                    CurrentAmount = x.CurrentAmount ?? 0,
                    AmountLimit = x.AmountLimit ?? 0,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,
                    ImageUrls = x.Images.Where(img => !img.IsDeleted)
                                        .Select(img => img.UrlPath)
                                        .ToArray()
                })
                .ToArrayAsync();
        }
        public async Task<ChildResponseDTO[]> GetAllChildIsDeleteAsync()
        {
            return await _context.Children
                .Where(x => x.IsDeleted)
                .Select(x => new ChildResponseDTO
                {
                    Id = x.Id,
                    ChildName = x.ChildName,
                    HealthStatus = x.HealthStatus,
                    HouseId = x.HouseId,
                    SchoolId = x.SchoolId,
                    FacilitiesWalletId = x.FacilitiesWalletId,
                    SystemWalletId = x.SystemWalletId,
                    FoodStuffWalletId = x.FoodStuffWalletId,
                    HealthWalletId = x.HealthWalletId,
                    NecessitiesWalletId = x.NecessitiesWalletId,
                    Amount = x.Amount ?? 0,
                    CurrentAmount = x.CurrentAmount ?? 0,
                    AmountLimit = x.AmountLimit ?? 0,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,
                    ImageUrls = x.Images.Where(img => !img.IsDeleted)
                                     .Select(img => img.UrlPath)
                                     .ToArray()
                })
                .ToArrayAsync();
        }

        public ChildResponseDTO GetChildByIdWithImg(string childId)
        {
            var childDetails = _context.Children
                .Where(x => x.Id == childId && !x.IsDeleted) // Ensure the event is not deleted
                .Select(x => new ChildResponseDTO
                {
                    Id = x.Id,
                    ChildName = x.ChildName,
                    HealthStatus = x.HealthStatus,
                    HouseId = x.HouseId,
                    SchoolId = x.SchoolId,
                    FacilitiesWalletId = x.FacilitiesWalletId,
                    SystemWalletId = x.SystemWalletId,
                    FoodStuffWalletId = x.FoodStuffWalletId,
                    HealthWalletId = x.HealthWalletId,
                    NecessitiesWalletId = x.NecessitiesWalletId,
                    Amount = x.Amount ?? 0,
                    CurrentAmount = x.CurrentAmount ?? 0,
                    AmountLimit = x.AmountLimit ?? 0,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,
                    ImageUrls = x.Images.Where(img => !img.IsDeleted)
                                     .Select(img => img.UrlPath)
                                     .ToArray()
                })
                .FirstOrDefault();  // Get the first (or default) result

            return childDetails;
        }

        public async Task<ChildResponseDTO[]> SearchChildrenAsync(string searchTerm)
        {
            var query = _context.Children
                .Include(c => c.House)  // Nạp dữ liệu House liên kết với Child
                .Where(c => !c.IsDeleted); // Lọc các Children chưa bị xóa

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string[] searchTerms = searchTerm.Split(' ').ToArray();

                query = query.Where(c =>
                    searchTerms.All(term =>
                        (c.Id.ToString().Contains(term) ||
                         c.ChildName.Contains(term) ||
                         c.HealthStatus.Contains(term) ||
                         c.HouseId.Contains(term) ||
                         c.SchoolId.Contains(term) ||
                         c.Gender.Contains(term) ||
                         c.Status.Contains(term)
                        )
                    )
                );
            }

            var children = await query.ToListAsync(); // Lấy danh sách Children

            var result = children.Select(c => new ChildResponseDTO
            {
                Id = c.Id,
                ChildName = c.ChildName ?? string.Empty,
                HealthStatus = c.HealthStatus ?? string.Empty,
                HouseId = c.HouseId ?? string.Empty,
                SchoolId = c.SchoolId ?? string.Empty,
                FacilitiesWalletId = c.FacilitiesWalletId,
                SystemWalletId = c.SystemWalletId,
                FoodStuffWalletId = c.FoodStuffWalletId,
                HealthWalletId = c.HealthWalletId,
                NecessitiesWalletId = c.NecessitiesWalletId,
                Amount = c.Amount ?? 0,
                CurrentAmount = c.CurrentAmount ?? 0,
                AmountLimit = c.AmountLimit ?? 0,
                Gender = c.Gender ?? string.Empty,
                Dob = c.Dob,
                CreatedDate = c.CreatedDate,
                ModifiedDate = c.ModifiedDate,
                Status = c.Status ?? string.Empty,
                ImageUrls = c.Images
                    .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                    .Select(img => img.UrlPath)
                    .ToArray()
            }).ToArray();

            return result;
        }

        // Lấy toàn bộ danh sách các Child thuộc HouseId cụ thể.
        public async Task<List<Child>> GetChildByHouseIdAsync(string houseId)
        {
            return await _context.Children
                .Where(c => c.HouseId == houseId && (c.IsDeleted == null || c.IsDeleted == false))
                .ToListAsync();
        }

        // Đếm số lượng Child thuộc HouseId cụ thể.
        public async Task<int> CountChildrenByHouseIdAsync(string houseId)
        {
            return await _context.Children
                .Where(child => child.HouseId == houseId && !child.IsDeleted)
                .CountAsync();
        }


        //Dashboard phần tính Active children trong hệ thống
        public async Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

            // Đếm tổng số trẻ em đang active
            var totalActive = await _context.Children
                .Where(c => c.Status == "Active" && !c.IsDeleted)
                .CountAsync();

            // Tính số trẻ em được thêm mới trong tháng này
            var addedThisMonth = await _context.Children
                .Where(c => c.Status == "Active"
                        && !c.IsDeleted
                        && c.CreatedDate >= firstDayOfMonth)
                .CountAsync();

            // Tính số trẻ em bị xóa hoặc chuyển sang inactive trong tháng này
            var removedThisMonth = await _context.Children
                .Where(c => (c.Status != "Active" || c.IsDeleted)
                        && c.ModifiedDate >= firstDayOfMonth)
                .CountAsync();

            // Tính tăng giảm theo tháng
            var netChange = addedThisMonth - removedThisMonth;

            return new ActiveChildrenStatDTO
            {
                TotalActiveChildren = totalActive,
                ChangeThisMonth = netChange
            };
        }

        // Dashboard phần Children For Demographics
        public async Task<IEnumerable<Child>> GetChildrenForDemographics()
        {
            return await _context.Children
                .Where(x => !x.IsDeleted && x.Status == "Active")
                .Select(x => new Child
                {
                    Dob = x.Dob,
                    Gender = x.Gender
                })
                .AsNoTracking()
                .ToListAsync();
        }

        // Dashboard phần Child Trends
        public async Task<List<ChildTrendDTO>> GetChildTrendsByYearAsync(int year)
        {
            var monthAbbreviations = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Danh sách mặc định 12 tháng với Count = 0
            var allMonths = Enumerable.Range(1, 12)
                .Select(month => new ChildTrendDTO
                {
                    Month = monthAbbreviations[month - 1],
                    Year = year,
                    Count = 0
                })
                .ToList();

            var actualData = await _context.Children
                .Where(c => c.CreatedDate.HasValue && c.CreatedDate.Value.Year == year)
                .GroupBy(c => c.CreatedDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.Month, x => x.Count);

            // Áp dụng dữ liệu thực tế vào danh sách mặc định
            foreach (var month in allMonths)
            {
                var monthIndex = Array.IndexOf(monthAbbreviations, month.Month) + 1; // Tìm index (1-based)
                if (actualData.ContainsKey(monthIndex))
                {
                    month.Count = actualData[monthIndex];
                }
            }

            return allMonths;
        }

        public async Task<ChildDetailsDTO> GetChildDetails(string childId)
        {
            if (string.IsNullOrEmpty(childId))
            {
                throw new ArgumentException("Child ID cannot be null or empty");
            }

            var child = await _context.Children
                .Include(c => c.House)
                .Include(c => c.School)
                .FirstOrDefaultAsync(c => c.Id == childId);

            if (child == null)
            {
                throw new Exception("Child not found.");
            }

            var healthReports = await _context.HealthReports
                .Where(hr => hr.ChildId == childId)
                .Select(hr => new HealthReportSummaryDTO
                {
                    Id = hr.Id,
                    NutritionalStatus = hr.NutritionalStatus ?? "Not Specified",
                    MedicalHistory = hr.MedicalHistory ?? "Not Specified",
                    VaccinationStatus = hr.VaccinationStatus ?? "Not Specified",
                    Weight = hr.Weight,
                    Height = hr.Height,
                    HealthStatus = hr.HealthStatus ?? "Not Specified",
                    Illnesses = hr.Illnesses ?? "None",
                    Allergies = hr.Allergies ?? "None"
                })
                .ToListAsync();

            var academicReports = await _context.AcademicReports
                .Where(ar => ar.ChildId == childId)
                .Select(ar => new AcademicReportSummaryDTO
                {
                    Id = ar.Id,
                    Diploma = ar.Diploma ?? "Not Specified",
                    SchoolLevel = ar.SchoolLevel ?? "Not Specified",
                    Gpa = ar.Gpa,
                    Semester = ar.Semester ?? "Not Specified",
                    AcademicYear = ar.AcademicYear ?? "Not Specified",
                    Achievement = ar.Achievement ?? "Not Specified",
                    SubjectDetails = _context.SubjectDetails
                        .Where(sd => sd.AcademicReportId == ar.Id && !sd.IsDeleted)
                        .Select(sd => new SubjectSummaryDTO
                        {
                            Id = sd.Id,
                            SubjectName = sd.SubjectName ?? "Not Specified",
                            Score = sd.Score,
                            Remarks = sd.Remarks ?? "Not Specified"
                        })
                        .ToList()
                })
                .ToListAsync();

            var childProgresses = await _context.ChildProgresses
                .Where(cp => cp.ChildId == childId && !cp.IsDeleted)
                .Select(cp => new ChildProgressSummaryDTO
                {
                    Id = cp.Id,
                    Description = cp.Description ?? "Not Specified",
                    Date = cp.Date,
                    Category = cp.Category ?? "Not Specified",
                    EventId = cp.EventId,
                    ActivityId = cp.ActivityId,
                    EventName = cp.Event != null ? cp.Event.Name : "No Event",
                    ActivityName = cp.Activity != null ? cp.Activity.ActivityName : "No Activity"
                })
                .ToListAsync();

            var result = new ChildDetailsDTO
            {
                Id = child.Id,
                ChildName = child.ChildName ?? "Unknown",
                HealthStatus = child.HealthStatus ?? "Unknown",
                Gender = child.Gender ?? "Unknown",
                Dob = child.Dob,
                Status = child.Status ?? "Unknown",
                HouseName = child.House?.HouseName ?? "Unknown",
                SchoolName = child.School?.SchoolName ?? "Unknown",
                HealthReports = healthReports,
                AcademicReports = academicReports,
                ChildProgresses = childProgresses,
            };

            return result;
        }

        public async Task<List<Child>> SearchChildren(SearchChildDTO searchChildDTO)
        {
            var query = _context.Children.AsQueryable();

            // Nếu có SearchTerm, tìm kiếm trong các cột cần tìm
            if (!string.IsNullOrEmpty(searchChildDTO.SearchTerm))
            {
                query = query.Where(x =>
                    (x.Id.ToString().Contains(searchChildDTO.SearchTerm) ||
                     x.ChildName.Contains(searchChildDTO.SearchTerm) ||
                     x.HealthStatus.Contains(searchChildDTO.SearchTerm) ||
                     x.HouseId.Contains(searchChildDTO.SearchTerm) ||
                     x.SchoolId.Contains(searchChildDTO.SearchTerm) ||
                     x.Gender.Contains(searchChildDTO.SearchTerm) ||
                     x.Status.Contains(searchChildDTO.SearchTerm)
                    )
                );
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Child>> GetChildrenWithBadHealthAsync(string houseId)
        {
            return await _context.Children
                .Where(c => c.HouseId == houseId && c.HealthStatus == "Bad")
                .ToListAsync();
        }

        public async Task<List<Child>> GetChildrenByIdsAsync(List<string> childIds)
        {
            return await _context.Children
                .Where(c => childIds.Contains(c.Id) && !c.IsDeleted && c.HealthStatus == "Bad") // Chỉ lấy trẻ có HealthStatus là "Bad"
                .ToListAsync();
        }

        public async Task<IEnumerable<Child>> GetChildrenWithRelationsAsync()
        {
            return await _context.Children
                .Include(c => c.House)
                .Include(c => c.School)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }
    }
}
