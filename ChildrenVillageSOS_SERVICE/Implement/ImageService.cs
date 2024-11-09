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

        public async Task<List<string>> UploadChildImage(List<IFormFile> files, string ChildId)
        {
            // Kiểm tra danh sách file có rỗng hay không
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files to upload!");
            }

            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);

            // Danh sách URL ảnh sau khi upload
            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                // Kiểm tra định dạng file
                if (!file.ContentType.ToLower().StartsWith("image/"))
                {
                    throw new Exception($"File {file.FileName} is not an image!");
                }

                // Thiết lập các tham số upload
                var uploadParameters = new ImageUploadParams
                {
                    Folder = $"ChildImages/{ChildId}",
                    PublicId = $"{ChildId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                    File = new FileDescription(file.FileName)
                };

                // Đọc file vào memory stream
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
                }

                // Thực hiện upload ảnh lên Cloudinary
                var result = await _cloudinary.UploadAsync(uploadParameters);

                // Kiểm tra lỗi khi upload
                if (result.Error != null)
                {
                    throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
                }

                // Thêm URL ảnh vào danh sách
                imageUrls.Add(result.SecureUrl.ToString());
            }

            return imageUrls;
        }

        public async Task<List<string>> UploadHouseImage(List<IFormFile> files, string HouseId)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files to upload!");
            }

            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);

            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                if (!file.ContentType.ToLower().StartsWith("image/"))
                {
                    throw new Exception($"File {file.FileName} is not an image!");
                }

                var uploadParameters = new ImageUploadParams
                {
                    Folder = $"HouseImages/{HouseId}",
                    PublicId = $"{HouseId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                    File = new FileDescription(file.FileName)
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
                }

                var result = await _cloudinary.UploadAsync(uploadParameters);

                if (result.Error != null)
                {
                    throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
                }

                imageUrls.Add(result.SecureUrl.ToString());
            }

            return imageUrls;
        }

        public async Task<List<string>> UploadVillageImage(List<IFormFile> files, string VillageId)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files to upload!");
            }

            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);

            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                if (!file.ContentType.ToLower().StartsWith("image/"))
                {
                    throw new Exception($"File {file.FileName} is not an image!");
                }

                var uploadParameters = new ImageUploadParams
                {
                    Folder = $"VillageImages/{VillageId}",
                    PublicId = $"{VillageId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                    File = new FileDescription(file.FileName)
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
                }

                var result = await _cloudinary.UploadAsync(uploadParameters);

                if (result.Error != null)
                {
                    throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
                }

                imageUrls.Add(result.SecureUrl.ToString());
            }

            return imageUrls;
        }

        public async Task<List<string>> UploadEventImage(List<IFormFile> files, int EventId)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files to upload!");
            }

            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);

            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                if (!file.ContentType.ToLower().StartsWith("image/"))
                {
                    throw new Exception($"File {file.FileName} is not an image!");
                }

                var uploadParameters = new ImageUploadParams
                {
                    Folder = $"EventImages/{EventId}",
                    PublicId = $"{EventId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                    File = new FileDescription(file.FileName)
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
                }

                var result = await _cloudinary.UploadAsync(uploadParameters);

                if (result.Error != null)
                {
                    throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
                }

                imageUrls.Add(result.SecureUrl.ToString());
            }

            return imageUrls;
        }

        public async Task<List<string>> UploadUserAccountImage(List<IFormFile> files, string UserAccountId)
        {
            if (files == null || files.Count == 0)
            {
                throw new Exception("No files to upload!");
            }

            var account = new CloudinaryDotNet.Account(_cloudName, _apiKey, _apiSecret);
            _cloudinary = new Cloudinary(account);

            var imageUrls = new List<string>();

            foreach (var file in files)
            {
                if (!file.ContentType.ToLower().StartsWith("image/"))
                {
                    throw new Exception($"File {file.FileName} is not an image!");
                }

                var uploadParameters = new ImageUploadParams
                {
                    Folder = $"UserImages/{UserAccountId}",
                    PublicId = $"{UserAccountId}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                    File = new FileDescription(file.FileName)
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    uploadParameters.File = new FileDescription(file.FileName, new MemoryStream(memoryStream.ToArray()));
                }

                var result = await _cloudinary.UploadAsync(uploadParameters);

                if (result.Error != null)
                {
                    throw new Exception($"Error uploading image {file.FileName}: {result.Error.Message}");
                }

                imageUrls.Add(result.SecureUrl.ToString());
            }

            return imageUrls;
        }
    }
}
