using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{

    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;

        public ImageService(IImageRepository imageRepository, IConfiguration configuration)
        {
            _imageRepository = imageRepository;
            _configuration = configuration;
            _cloudinary = InitializeCloudinary();
        }

        private Cloudinary InitializeCloudinary()
        {
            var account = new Account(
                _configuration["Cloudinary:CloudName"],
                _configuration["Cloudinary:ApiKey"],
                _configuration["Cloudinary:ApiSecret"]
            );
            return new Cloudinary(account);
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _imageRepository.GetAllAsync();
        }

        private string ExtractPublicIdFromUrl(string url, string path)
        {
            if (!url.Contains(path))
            {
                throw new ArgumentException("Invalid file path");
            }

            var urlWithoutOrigin = url.Substring(url.IndexOf(path));
            var supportedExtensions = new[] { ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            return supportedExtensions
                .Where(ext => urlWithoutOrigin.EndsWith(ext))
                .Select(ext => urlWithoutOrigin.Substring(0, urlWithoutOrigin.Length - ext.Length))
                .FirstOrDefault() ?? urlWithoutOrigin;
        }

        public async Task<bool> DeleteImageAsync(string urlImage, string path)
        {
            try
            {
                var publicId = ExtractPublicIdFromUrl(urlImage, path);
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception($"Error deleting image: {result.Error?.Message}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting image: {ex.Message}");
            }
        }

        public async Task<List<string>> UploadImages(List<IFormFile> files, string entityId, string entityType)
        {
            ValidateFiles(files);
            var uploadTasks = files.Select(file => UploadSingleImage(file, entityId, entityType));
            return await Task.WhenAll(uploadTasks).ContinueWith(t => t.Result.ToList());
        }

        private void ValidateFiles(List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                throw new ArgumentException("No files to upload!");
            }

            var invalidFiles = files.Where(f => !f.ContentType.ToLower().StartsWith("image/")).ToList();
            if (invalidFiles.Any())
            {
                throw new ArgumentException($"Invalid file types: {string.Join(", ", invalidFiles.Select(f => f.FileName))}");
            }
        }

        private async Task<string> UploadSingleImage(IFormFile file, string entityId, string entityType)
        {
            var uploadParams = CreateUploadParameters(file, entityId, entityType);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParams.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
            }

            return result.SecureUrl.ToString();
        }

        private ImageUploadParams CreateUploadParameters(IFormFile file, string entityId, string entityType)
        {
            return new ImageUploadParams
            {
                Folder = $"{entityType}Images/{entityId}",
                PublicId = $"{entityId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                File = new FileDescription(file.FileName)
            };
        }

        // Implement specific upload methods using the generic upload method
        public Task<List<string>> UploadChildImage(List<IFormFile> files, string childId)
            => UploadImages(files, childId, "Child");

        public Task<List<string>> UploadHouseImage(List<IFormFile> files, string houseId)
            => UploadImages(files, houseId, "House");

        public Task<List<string>> UploadVillageImage(List<IFormFile> files, string villageId)
            => UploadImages(files, villageId, "Village");

        public Task<List<string>> UploadEventImage(List<IFormFile> files, int eventId)
            => UploadImages(files, eventId.ToString(), "Event");

        public Task<List<string>> UploadUserAccountImage(List<IFormFile> files, string userAccountId)
            => UploadImages(files, userAccountId, "UserAccount");

        public Task<List<string>> UploadIventoryImage(List<IFormFile> files, string inventoryId)
            => UploadImages(files, inventoryId, "Inventory");

        public Task<List<string>> UploadActivityImage(List<IFormFile> files, string activityId)
            => UploadImages(files, activityId, "Activity");

        public Task<List<string>> UploadSchoolImage(List<IFormFile> files, string schoolId)
            => UploadImages(files, schoolId, "School");
    }
}
