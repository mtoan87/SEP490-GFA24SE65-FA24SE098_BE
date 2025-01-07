using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.InventoryDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Helpers;
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
    public class AcademicReportService : IAcademicReportService
    {
        private readonly IAcademicReportRepository _academicReportRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public AcademicReportService(IAcademicReportRepository academicReportRepository,
            IImageService imageService,
            IImageRepository imageRepository)
        {
            _academicReportRepository = academicReportRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<AcademicReport>> GetAllAcademicReports()
        {
            return await _academicReportRepository.GetAllAsync();
        }

        public Task<AcademicReportResponseDTO[]> GetAllAcademicReportIsDeleteAsync()
        {
            return _academicReportRepository.GetAllAcademicReportIsDeleteAsync();
        }

        public async Task<IEnumerable<AcademicReportResponseDTO>> GetAllAcademicReportWithImg()
        {
            var academicReports = await _academicReportRepository.GetAllNotDeletedAsync();

            var academicReportResponseDTOs = academicReports.Select(ar => new AcademicReportResponseDTO
            {
                Id = ar.Id,
                Diploma = ar.Diploma,
                SchoolLevel = ar.SchoolLevel,
                ChildId = ar.ChildId,
                SchoolId = ar.SchoolId,
                Gpa = ar.Gpa,
                SchoolReport = ar.SchoolReport,
                Semester = ar.Semester,
                AcademicYear = ar.AcademicYear,
                Remarks = ar.Remarks,
                Achievement = ar.Achievement,
                Status = ar.Status,
                Class = ar.Class,
                Feedback = ar.Feedback,
                IsDeleted = ar.IsDeleted,
                CreatedBy = ar.CreatedBy,
                CreatedDate = ar.CreatedDate,
                ModifiedBy = ar.ModifiedBy,
                ModifiedDate = ar.ModifiedDate,
                ImageUrls = ar.Images.Where(img => !img.IsDeleted) // Lọc hình ảnh chưa bị xóa
                                .Select(img => img.UrlPath)
                                .ToArray()
            }).ToArray();

            return academicReportResponseDTOs;
        }


        public async Task<AcademicReport> GetAcademicReportById(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }
            return report;
        }

        public async Task<AcademicReportResponseDTO> GetAcademicReportByIdWithImg(int id)
        {
            return _academicReportRepository.GetAcademicReportByIdWithImg(id);
        }

        public async Task<AcademicReport> CreateAcademicReport(CreateAcademicReportDTO createReport)
        {
            var existingReport = await _academicReportRepository.FindAsync(r => r.ChildId == createReport.ChildId);

            if (existingReport != null)
            {
                throw new InvalidOperationException($"An academic report already exists for ChildId {createReport.ChildId}.");
            }

            var newReport = new AcademicReport
            {
                Diploma = createReport.Diploma,
                SchoolLevel = createReport.SchoolLevel,
                ChildId = createReport.ChildId,
                SchoolId = createReport.SchoolId,
                Gpa = createReport.Gpa,
                SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(createReport.Gpa),
                Semester = createReport.Semester,
                AcademicYear = createReport.AcademicYear,
                Remarks = createReport.Remarks,
                Achievement = createReport.Achievement,
                Status = createReport.Status,
                Class = createReport.Class,
                Feedback = createReport.Feedback,
                CreatedBy = createReport.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false

            };

            await _academicReportRepository.AddAsync(newReport);

            // Upload danh sách hình ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadAcademicReportImage(createReport.Img, newReport.Id);

            // Lưu thông tin các hình ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    AcademicReportId = newReport.Id, // Liên kết với AcademicReport
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }

            return newReport;
        }

        public async Task<AcademicReport> UpdateAcademicReport(int id, UpdateAcademicReportDTO updateReport)
        {
            var editReport = await _academicReportRepository.GetByIdAsync(id);
            if (editReport == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }

            editReport.Diploma = updateReport.Diploma;
            editReport.SchoolLevel = updateReport.SchoolLevel;
            editReport.ChildId = updateReport.ChildId;
            editReport.SchoolId = updateReport.SchoolId;
            editReport.Gpa = updateReport.Gpa;
            editReport.SchoolReport = SchoolReportGenerator.GetSchoolReportFromGPA(editReport.Gpa);
            editReport.Semester = updateReport.Semester;
            editReport.AcademicYear = updateReport.AcademicYear;
            editReport.Remarks = updateReport.Remarks;
            editReport.Achievement = updateReport.Achievement;
            editReport.Status = updateReport.Status;
            editReport.Class = updateReport.Class;
            editReport.Feedback = updateReport.Feedback;
            editReport.ModifiedBy = updateReport.ModifiedBy;
            editReport.ModifiedDate = DateTime.Now;

            var existingImages = await _imageRepository.GetByAcademicReportIdAsync(editReport.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateReport.ImgToDelete != null && updateReport.ImgToDelete.Any())
            {
                foreach (var imageUrlToDelete in updateReport.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageUrlToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "AcademicReportImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateReport.Img != null && updateReport.Img.Any())
            {
                var newImageUrls = await _imageService.UploadAcademicReportImage(updateReport.Img, editReport.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        AcademicReportId = editReport.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _academicReportRepository.UpdateAsync(editReport);
            return editReport;
        }

        public async Task<AcademicReport> DeleteAcademicReport(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }

            if (report.IsDeleted)
            {
                // Hard delete nếu báo cáo đã bị soft delete
                await _academicReportRepository.RemoveAsync(report);
            }
            else
            {
                // Soft delete (đặt IsDeleted = true)
                report.IsDeleted = true;
                await _academicReportRepository.UpdateAsync(report);
            }

            return report;
        }

        public async Task<AcademicReport> RestoreAcademicReport(int id)
        {
            var report = await _academicReportRepository.GetByIdAsync(id);
            if (report == null)
            {
                throw new Exception($"Academic report with ID {id} not found!");
            }

            if (report.IsDeleted)
            {
                // Khôi phục bằng cách đặt lại IsDeleted = false
                report.IsDeleted = false;
                await _academicReportRepository.UpdateAsync(report);
            }

            return report;
        }

        public async Task<List<AcademicReport>> SearchAcademicReports(SearchAcademicReportDTO searchAcademicReportDTO)
        {
            return await _academicReportRepository.SearchAcademicReports(searchAcademicReportDTO);
        }
    }
}
