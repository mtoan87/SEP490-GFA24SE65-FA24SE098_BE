using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.SchoolDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;

        public SchoolService(ISchoolRepository schoolRepository,
            IImageService imageService,
            IImageRepository imageRepository)
        {
            _schoolRepository = schoolRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<School>> GetAllSchools()
        {
            return await _schoolRepository.GetAllNotDeletedAsync();
        }

        public Task<SchoolResponseDTO[]> GetAllSchoolsIsDeleted()
        {
            return _schoolRepository.GetAllSchoolsIsDeleted();
        }
        
        public async Task<SchoolDetailsDTO> GetSchoolDetails(string schoolId)
        {
            return await _schoolRepository.GetSchoolDetails(schoolId);
        }

        public async Task<School> GetSchoolById(string id)
        {
            return await _schoolRepository.GetByIdAsync(id);
        }

        public async Task<SchoolResponseDTO> GetSchoolByIdWithImg(string schoolId)
        {
            return _schoolRepository.GetSchoolByIdWithImg(schoolId);
        }

        public async Task<IEnumerable<SchoolResponseDTO>> GetAllSchoolWithImg()
        {
            // Lấy tất cả các school không bị xóa từ repository, bao gồm liên kết tới hình ảnh
            var schools = await _schoolRepository.GetAllNotDeletedAsync();

            var schoolResponseDTOs = schools.Select(school => new SchoolResponseDTO
            {
                Id = school.Id,
                SchoolName = school.SchoolName,
                Address = school.Address,
                SchoolType = school.SchoolType,
                PhoneNumber = school.PhoneNumber,
                Email = school.Email,
                IsDeleted = school.IsDeleted,
                CreatedBy = school.CreatedBy,
                CreatedDate = school.CreatedDate,
                ModifiedBy = school.ModifiedBy,
                ModifiedDate = school.ModifiedDate,
                ImageUrls = school.Images
                    .Where(img => !img.IsDeleted) // Lọc các hình ảnh chưa bị xóa
                    .Select(img => img.UrlPath)  // Lấy đường dẫn URL
                    .ToArray() // Chuyển thành mảng
            }).ToArray();

            return schoolResponseDTOs;
        }

        public async Task<School> CreateSchool(CreateSchoolDTO createSchool)
        {
            var newSchool = new School
            {
                SchoolName = createSchool.SchoolName,
                Address = createSchool.Address,
                SchoolType = createSchool.SchoolType,
                PhoneNumber = createSchool.PhoneNumber,
                Email = createSchool.Email,
                CreatedBy = createSchool.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _schoolRepository.AddAsync(newSchool);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadSchoolImage(createSchool.Img, newSchool.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    SchoolId = newSchool.Id, // Liên kết với School
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }

            return newSchool;
        }

        public async Task<School> UpdateSchool(string id, UpdateSchoolDTO updateSchool)
        {
            var existingSchool = await _schoolRepository.GetByIdAsync(id);
            if (existingSchool == null)
            {
                throw new Exception($"School with ID {id} not found!");
            }

            existingSchool.SchoolName = updateSchool.SchoolName;
            existingSchool.Address = updateSchool.Address;
            existingSchool.SchoolType = updateSchool.SchoolType;
            existingSchool.PhoneNumber = updateSchool.PhoneNumber;
            existingSchool.Email = updateSchool.Email;
            existingSchool.ModifiedBy = updateSchool.ModifiedBy;
            existingSchool.ModifiedDate = DateTime.Now;

            // Lấy danh sách ảnh hiện tại
            var existingImages = await _imageRepository.GetBySchoolIdAsync(existingSchool.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateSchool.ImgToDelete != null && updateSchool.ImgToDelete.Any())
            {
                foreach (var imageIdToDelete in updateSchool.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "SchoolImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateSchool.Img != null && updateSchool.Img.Any())
            {
                var newImageUrls = await _imageService.UploadSchoolImage(updateSchool.Img, existingSchool.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        SchoolId = existingSchool.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _schoolRepository.UpdateAsync(existingSchool);
            return existingSchool;
        }

        public async Task<School> DeleteSchool(string id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                throw new Exception($"School with ID {id} not found");
            }

            if (school.IsDeleted)
            {
                await _schoolRepository.RemoveAsync(school);
            }
            else
            {
                school.IsDeleted = true;
                await _schoolRepository.UpdateAsync(school);
            }

            return school;
        }

        public async Task<School> RestoreSchool(string id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                throw new Exception($"School with ID {id} not found");
            }

            if (school.IsDeleted)
            {
                school.IsDeleted = false;
                await _schoolRepository.UpdateAsync(school);
            }

            return school;
        }
    }
}
