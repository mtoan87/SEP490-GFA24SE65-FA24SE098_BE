using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.HouseDTO;
using ChildrenVillageSOS_DAL.Enum;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ChildService : IChildService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IChildRepository _childRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly IDonationService _donationService;
        private readonly IFacilitiesWalletRepository _failitiesWalletRepository;
        private readonly IFoodStuffWalletRepository _foodStuffWalletRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletRepository;
        private readonly ISystemWalletRepository _systemWalletRepository;
        private readonly IHealthWalletRepository _healthWalletRepository;
        private readonly IIncomeRepository _incomeRepository;


        public ChildService(IChildRepository childRepository,
            IUserAccountRepository userAccountRepository,
            IImageService imageService,
            IImageRepository imageRepository,                   
            IPaymentRepository paymentRepository,              
            IConfiguration configuration,
            IDonationService donationService,
            IFacilitiesWalletRepository facilitiesWalletRepository,           
            ISystemWalletRepository systemWalletRepository,
            INecessitiesWalletRepository necessitiesWalletRepository,            
            IFoodStuffWalletRepository foodStuffWalletRepository,
            IHealthWalletRepository healthWalletRepository,
            IIncomeRepository incomeRepository)
        {
            _userAccountRepository = userAccountRepository;
            _childRepository = childRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;         
            _paymentRepository = paymentRepository;           
            _configuration = configuration;
            _donationService = donationService;
            _failitiesWalletRepository = facilitiesWalletRepository;          
            _systemWalletRepository = systemWalletRepository;
            _foodStuffWalletRepository = foodStuffWalletRepository;
            _necessitiesWalletRepository = necessitiesWalletRepository;
            _healthWalletRepository = healthWalletRepository;
            _incomeRepository = incomeRepository;

        }

        public async Task<IEnumerable<Child>> GetAllChildren()
        {
            return await _childRepository.GetAllNotDeletedAsync();
        }

        public async Task<IEnumerable<ChildResponseDTO>> GetAllChildrenWithImg()
        {
            var childs = await _childRepository.GetAllNotDeletedAsync();
            
            var childResponseDTOs = childs.Select(x => new ChildResponseDTO
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
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                ImageUrls = x.Images.Where(img => !img.IsDeleted)  
                                     .Select(img => img.UrlPath)  
                                     .ToArray()
            }).ToArray();

            return childResponseDTOs;
        }
        public async Task<IEnumerable<ChildResponseDTO>> GetAllChildrenWithHealthStatusBad()
        {
            var childs = await _childRepository.GetAllNotDeletedAsync();

            var childResponseDTOs = childs
                .Where(x => x.HealthStatus == "Bad")
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
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                ImageUrls = x.Images.Where(img => !img.IsDeleted)
                                     .Select(img => img.UrlPath)
                                     .ToArray()
            }).ToArray();

            return childResponseDTOs;

        }

        // GET ID
        public async Task<Child> GetChildById(string id)
        {
            return await _childRepository.GetByIdAsync(id);
        }

        public async Task<List<Child>> GetChildByHouseIdAsync(string houseId)
        {
            return await _childRepository.GetChildByHouseIdAsync(houseId);
        }
        public async Task<ChildResponseDTO[]> GetChildByHouseId(string houseId)
        {
            return await _childRepository.GetChildByHouseId(houseId);
        }

        public async Task<ChildDetailsDTO> GetChildDetails(string childId)
        {
            return await _childRepository.GetChildDetails(childId);
        }

        public async Task<ChildResponseDTO> GetChildByIdWithImg(string childid)
        {
            return _childRepository.GetChildByIdWithImg(childid);
        }

        public async Task<Child> CreateChild(CreateChildDTO createChild)
        {
            // Lấy toàn bộ danh sách ChildId hiện có
            var allChildIds = await _childRepository.Entities()
                                                    .Select(c => c.Id)
                                                    .ToListAsync();

            // Sử dụng hàm GenerateId từ IdGenerator
            string newChildId = IdGenerator.GenerateId(allChildIds, "C");

            var newChild = new Child
            {
                Id = newChildId,  // Gán ID mới
                ChildName = createChild.ChildName,
                HealthStatus = createChild.HealthStatus,
                FacilitiesWalletId = createChild.FacilitiesWalletId,
                SystemWalletId = createChild.SystemWalletId,
                FoodStuffWalletId = createChild.FoodStuffWalletId,
                HealthWalletId = createChild.HealthWalletId,
                NecessitiesWalletId = createChild.NecessitiesWalletId,
                Amount = createChild.Amount,
                CurrentAmount = createChild.Amount,
                AmountLimit = createChild.AmountLimit,
                HouseId = createChild.HouseId,
                SchoolId = createChild.SchoolId,
                Gender = createChild.Gender,
                Dob = createChild.Dob,
                CreatedDate = DateTime.Now,
                Status = createChild.Status,
                IsDeleted = false
                
            };
            await _childRepository.AddAsync(newChild);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadChildImage(createChild.Img, newChild.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    ChildId = newChild.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
            return newChild;
        }
        public async Task<string> DonateChild(string id, ChildDonateDTO updateChild)
        {
            // Step 1: Retrieve the event by ID
            var editChild = await _childRepository.GetByIdAsync(id);
            if (editChild == null)
            {
                throw new Exception($"Child with ID {id} not found!");
            }

            // Step 2: Check if new donation will exceed the AmountLimit
            var newTotalAmount = (editChild.CurrentAmount ?? 0) + (updateChild.Amount ?? 0);
            if (newTotalAmount > (editChild.AmountLimit ?? 0))
            {
                throw new InvalidOperationException("Donation amount exceeds the allowed limit.");
            }




            // Step 4: Create Donation
            string? userName, userEmail, address;
            long? phone;

            // Lấy thông tin người dùng từ UserRepository nếu UserAccountId khác null
            if (!string.IsNullOrEmpty(updateChild.UserAccountId))
            {
                var user = await _userAccountRepository.GetByIdAsync(updateChild.UserAccountId);
                if (user != null)
                {
                    userName = user.UserName;
                    userEmail = user.UserEmail;
                    phone = user.Phone;
                    address = user.Address;
                }
                else
                {
                    throw new Exception("User not found.");
                }
            }
            else
            {
                // Lấy thông tin từ request
                userName = updateChild.UserName;
                userEmail = updateChild.UserEmail;
                phone = updateChild.Phone;
                address = updateChild.Address;
            }



            // Tạo đối tượng DonationDTO
            var donationDto = new DonateDTO
            {
                UserAccountId = updateChild.UserAccountId,
                ChildId = editChild.Id,
               
                UserName = userName,
                UserEmail = userEmail,
                Phone = phone,
                Address = address,
                FoodStuffWalletId = editChild.FoodStuffWalletId,
                FacilitiesWalletId = editChild.FacilitiesWalletId,
                NecessitiesWalletId = editChild.NecessitiesWalletId,
                HealthWalletId = editChild.HealthWalletId,
                DonationType = DonateType.Child.ToString(),
                DateTime = DateTime.Now,
                Amount = updateChild.Amount ?? 0,
                Description = updateChild.Description,
                IsDeleted = false,
                Status = DonateStatus.Pending.ToString(),
            };
            var donation = await _donationService.DonateNow(donationDto);

            // Step 5: Create VNPay URL for payment
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (updateChild.Amount.GetValueOrDefault() * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData(
            "vnp_OrderInfo",
            $"Thanh toán cho Donation {donation.Id}, childId {id}, " +
            string.Join(", ", new[]
            {
            editChild.FacilitiesWalletId != null ? $"walletId {editChild.FacilitiesWalletId}" : null,
            editChild.FoodStuffWalletId != null ? $"walletId {editChild.FoodStuffWalletId}" : null,
            editChild.NecessitiesWalletId != null ? $"walletId {editChild.NecessitiesWalletId}" : null,
            editChild.HealthWalletId != null ? $"walletId {editChild.HealthWalletId}" : null
            }.Where(s => s != null))
            );
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            string uniqueTxnRef = $"{donation.Id}_{DateTime.Now.Ticks}";
            vnpay.AddRequestData("vnp_TxnRef", uniqueTxnRef);
            //vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            //vnpay.AddRequestData("childId", id);
            //vnpay.AddRequestData("walletId", editChild.FacilitiesWalletId?.ToString() ?? string.Empty);
            //vnpay.AddRequestData("walletId", editChild.FoodStuffWalletId?.ToString() ?? string.Empty);
            //vnpay.AddRequestData("walletId", editChild.SystemWalletId?.ToString() ?? string.Empty);
            //vnpay.AddRequestData("walletId", editChild.HealthWalletId?.ToString() ?? string.Empty);
            //vnpay.AddRequestData("walletId", editChild.NecessitiesWalletId?.ToString() ?? string.Empty);
            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);                                 
            var income = new Income
            {
                UserAccountId = updateChild.UserAccountId,
                FacilitiesWalletId = editChild.FacilitiesWalletId,
                FoodStuffWalletId = editChild.FoodStuffWalletId,
                SystemWalletId = editChild.SystemWalletId,
                HealthWalletId = editChild.HealthWalletId,
                NecessitiesWalletId = editChild.NecessitiesWalletId,
                Amount = updateChild.Amount ?? 0,
                Receiveday = DateTime.Now,
                Status = IncomeStatus.Pending.ToString(),
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 8: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = updateChild.Amount ?? 0,
                PaymentMethod = PaymentMethod.Banking.ToString(),
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = DonateStatus.Pending.ToString(),
            };
            await _paymentRepository.AddAsync(payment);

            return paymentUrl;
        }
        public async Task<Child> UpdateChild(string id, UpdateChildDTO updateChild)
        {
            var existingChild = await _childRepository.GetByIdAsync(id);
            if (existingChild == null)
            {
                throw new Exception($"Child with ID {id} not found!");
            }

            existingChild.ChildName = updateChild.ChildName;
            existingChild.HealthStatus = updateChild.HealthStatus;
            existingChild.HouseId = updateChild.HouseId;
            existingChild.SchoolId = updateChild.SchoolId;
            existingChild.Gender = updateChild.Gender;
            existingChild.Dob = updateChild.Dob;
            existingChild.Status = updateChild.Status;
            existingChild.IsDeleted = false;
            existingChild.ModifiedDate = DateTime.Now;

            var existingImages = await _imageRepository.GetByChildIdAsync(existingChild.Id);

            if (updateChild.ImgToDelete != null && updateChild.ImgToDelete.Any())
            {
                // Có thể xóa được ảnh đang tòn tại trong khi đang update
                foreach (var imageIdToDelete in updateChild.ImgToDelete)
                {
                    try
                    {
                        Console.WriteLine($"Attempting to delete image with ID: {imageIdToDelete}");

                        // Tìm ảnh theo URLPath để xóa
                        var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);

                        if (imageToDelete != null)
                        {
                            Console.WriteLine($"Found image to delete: {imageToDelete.UrlPath}");

                            imageToDelete.IsDeleted = true;
                            imageToDelete.ModifiedDate = DateTime.Now;
                            await _imageRepository.UpdateAsync(imageToDelete);

                            bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "ChildImages");
                            Console.WriteLine($"Image deletion from Cloudinary: {isDeleted}");

                            if (isDeleted)
                            {
                                await _imageRepository.RemoveAsync(imageToDelete);
                                Console.WriteLine("Image removed from database");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Image with URL {imageIdToDelete} not found in existingImages");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Lỗi khi xóa ảnh: {ex.Message}");
                    }
                }
            }

            // Nếu có ảnh mới cần thêm
            if (updateChild.Img != null && updateChild.Img.Any())
            {
                // Upload danh sách ảnh mới và lưu thông tin vào database
                List<string> newImageUrls = await _imageService.UploadChildImage(updateChild.Img, existingChild.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        ChildId = existingChild.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            // Lưu thay đổi
            await _childRepository.UpdateAsync(existingChild);
            return existingChild;
        }


        //public async Task<Child> UpdateChild(string id, UpdateChildDTO updateChild)
        //{
        //    var existingChild = await _childRepository.GetByIdAsync(id);
        //    if (existingChild == null)
        //    {
        //        throw new Exception($"Child with ID {id} not found!");
        //    }

        //    // Cập nhật các thuộc tính cơ bản
        //    existingChild.ChildName = updateChild.ChildName;
        //    existingChild.HealthStatus = updateChild.HealthStatus;
        //    existingChild.HouseId = updateChild.HouseId;
        //    existingChild.Gender = updateChild.Gender;
        //    existingChild.Dob = updateChild.Dob;
        //    existingChild.Status = updateChild.Status;
        //    existingChild.IsDeleted =false;
        //    existingChild.ModifiedDate = DateTime.Now;

        //    // Nếu có danh sách ảnh được upload trong yêu cầu cập nhật
        //    if (updateChild.Img != null && updateChild.Img.Any())
        //    {
        //        // Lấy danh sách ảnh hiện tại của CHild từ database Image
        //        var existingImages = await _imageRepository.GetByChildIdAsync(existingChild.Id);

        //        // Xóa tất cả các ảnh cũ trên Cloudinary và trong cơ sở dữ liệu
        //        foreach (var existingImage in existingImages)
        //        {
        //            // Xóa ảnh trên Cloudinary
        //            bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "ChildImages");
        //            if (!isDeleted)
        //            {
        //                throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
        //            }
        //            // Xóa ảnh khỏi database
        //            await _imageRepository.RemoveAsync(existingImage);
        //        }
        //    }

        //    // Upload danh sách ảnh mới và lưu thông tin vào database
        //    List<string> newImageUrls = await _imageService.UploadChildImage(updateChild.Img, existingChild.Id);
        //    foreach (var newImageUrl in newImageUrls)
        //    {
        //        var newImage = new Image
        //        {
        //            UrlPath = newImageUrl,
        //            ChildId = existingChild.Id,
        //            ModifiedDate = DateTime.Now,
        //            IsDeleted = false,
        //        };
        //        await _imageRepository.AddAsync(newImage);
        //    }

        //    // Lưu thay đổi
        //    await _childRepository.UpdateAsync(existingChild);
        //    return existingChild;
        //}

        public async Task<Child> DeleteChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true)
            {
                // Hard delete
                await _childRepository.RemoveAsync(child);
            }
            else
            {
                // Soft delete (đặt IsDeleted = true)
                child.IsDeleted = true;
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }

        public async Task<Child> RestoreChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true) // Nếu đã bị soft delete
            {
                child.IsDeleted = false;  // Khôi phục bằng cách đặt lại IsDeleted = false
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }

        public Task<ChildResponseDTO[]> GetAllChildIsDeleteAsync()
        {
            return _childRepository.GetAllChildIsDeleteAsync();
        }

    }
}
