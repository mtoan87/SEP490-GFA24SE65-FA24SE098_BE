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
        private Cloudinary _cloudinary;
        private string _cloudName;
        private string _apiKey;
        private string _apiSecret;
        private readonly IConfiguration _configuration;
        public ImageService(IImageRepository imageRepository, Cloudinary cloudinary, IConfiguration configuration)
        {
            _imageRepository = imageRepository;
            _cloudinary = cloudinary;
            _configuration = configuration;
            _cloudName = _configuration["Cloudinary:CloudName"]!;
            _apiKey = _configuration["Cloudinary:ApiKey"]!;
            _apiSecret = _configuration["Cloudinary:ApiSecret"]!;
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _imageRepository.GetAllAsync();
        }

        private string ExtractPublicIdFromUrl(string url, string path)
        {
            // Delete origin url
            int index = url.IndexOf(path);
            if (index != -1)
            {
                url = url.Substring(index);
            }
            else
            {
                throw new Exception("Đường dẫn tệp bị sai");
            }
            // Delete extension url
            string[] extensions = { ".bmp", ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            foreach (string extension in extensions)
                if (url.EndsWith(extension))
                {
                    url = url.Substring(0, url.Length - extension.Length);
                    break;
                }
            return url;
        }

        public async Task<bool> DeleteImageAsync(string urlImage, string path)
        {
            try
            {
                string publicId = ExtractPublicIdFromUrl(urlImage, path);

                var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
                _cloudinary = new Cloudinary(account);

                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error deleting image: {result.Error.Message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting image: {ex.Message}");
            }
        }

        public async Task<string> UploadChildImage(IFormFile file, string ChildId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                // Tạo một folder riêng 
                Folder = $"ChildImages/{ChildId}",
                PublicId = $"{ChildId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }

        public async Task<string> UploadHouseImage(IFormFile file, string HouseId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                Folder = $"HouseImages/{HouseId}",
                PublicId = $"{HouseId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }

        public async Task<string> UploadVillageImage(IFormFile file, string VillageId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                Folder = $"VillageImages/{VillageId}",
                PublicId = $"{VillageId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }

        public async Task<string> UploadEventImage(IFormFile file, int EventId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                Folder = $"EventImages/{EventId}",
                PublicId = $"{EventId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }

        public async Task<string> UploadUserAccountImage(IFormFile file, string UserAccountId)
        {
            if (!file.ContentType.ToLower().StartsWith("image/"))
            {
                throw new Exception("File is not a image!");
            }
            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);
            var uploadParameters = new ImageUploadParams
            {
                Folder = $"UserImages/{UserAccountId}",
                PublicId = $"{UserAccountId}_{Path.GetFileNameWithoutExtension(file.FileName)}"
            };
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
            }
            var result = await _cloudinary.UploadAsync(uploadParameters);
            if (result.Error != null)
            {
                throw new Exception($"Error upload image: {result.Error.Message}");
            }
            return result.SecureUrl.ToString();
        }
    }
}
