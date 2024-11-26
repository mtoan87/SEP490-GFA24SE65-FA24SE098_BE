using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
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
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;        
        private readonly IPaymentRepository _paymentRepository;       
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly IDonationService _donationService;             
        private readonly IIncomeRepository _incomeRepository;
        public EventService(IEventRepository eventRepository,
            IImageService imageService,
            IImageRepository imageRepository,          
            IPaymentRepository paymentRepository,          
            IConfiguration configuration,
            IDonationService donationService,           
            IIncomeRepository incomeRepository)
        {
            _eventRepository = eventRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;         
            _paymentRepository = paymentRepository;    
            _configuration = configuration;
            _donationService = donationService;         
            _incomeRepository = incomeRepository;

        }

        public Task<EventResponseDTO[]> GetAllEventIsDeleteAsync()
        {
            return _eventRepository.GetAllEventIsDeleteAsync();
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEvent()
        {
            var events = await _eventRepository.GetAllNotDeletedAsync();

            // Sử dụng Include để tải hình ảnh liên quan đến mỗi Event
            var eventResponseDTOs = events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                Name = e.Name,
                FacilitiesWalletId = e.FacilitiesWalletId,
                HealthWalletId = e.HealthWalletId,
                SystemWalletId  = e.SystemWalletId,
                NecessitiesWalletId = e.NecessitiesWalletId,
                FoodStuffWalletId = e.FoodStuffWalletId,  
                Description = e.Description,
                StartTime = e.StartTime ?? DateTime.MinValue,
                EndTime = e.EndTime ?? DateTime.MinValue,
                Amount = e.Amount ?? 0,
                CurrentAmount = e.CurrentAmount ?? 0,   
                AmountLimit = e.AmountLimit ?? 0,
                Status = e.Status,
                VillageId = e.VillageId,
                CreatedDate = e.CreatedDate,
                // Lấy URL của tất cả các hình ảnh
                ImageUrls = e.Images.Where(img => !img.IsDeleted)  // Lọc hình ảnh chưa bị xóa
                                     .Select(img => img.UrlPath)   // Chỉ lấy UrlPath
                                     .ToArray()  // Chuyển thành mảng
            }).ToArray();

            return eventResponseDTOs;
        }



        public async Task<EventResponseDTO> GetEventById(int id)
        {
            return _eventRepository.GetEventById(id);
        }

        public async Task<Event> CreateEvent(CreateEventDTO createEvent)
        {
            var newEvent = new Event
            {
                Name = createEvent.Name,
                Description = createEvent.Description,
                FacilitiesWalletId = createEvent.FacilitiesWalletId,
                FoodStuffWalletId = createEvent.FoodStuffWalletId,
                SystemWalletId = createEvent.SystemWalletId,
                HealthWalletId = createEvent.HealthWalletId,
                NecessitiesWalletId = createEvent.NecessitiesWalletId,
                StartTime = createEvent.StartTime,
                EndTime = createEvent.EndTime,
                Status = "Active",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Amount = createEvent.Amount,
                CurrentAmount = createEvent.Amount,
                AmountLimit = createEvent.AmountLimit, 
                VillageId = createEvent.VillageId,
               
            };
            await _eventRepository.AddAsync(newEvent);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadEventImage(createEvent.Img, newEvent.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    EventId = newEvent.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
            return newEvent;
        }
        public async Task<Event> UpdateEvent(int id ,UpdateEventDTO updateEvent)
        {
            var editEvent = await _eventRepository.GetByIdAsync(id);
            if(editEvent == null)
            {
                throw new Exception($"Event with ID{id} not found!");
            }

            editEvent.Name = updateEvent.Name;
            editEvent.Description = updateEvent.Description;
            editEvent.FacilitiesWalletId = updateEvent.FacilitiesWalletId;
            editEvent.FoodStuffWalletId = updateEvent.FoodStuffWalletId;
            editEvent.SystemWalletId = updateEvent.SystemWalletId;
            editEvent.HealthWalletId = updateEvent.HealthWalletId;
            editEvent.NecessitiesWalletId = updateEvent.NecessitiesWalletId;
            editEvent.StartTime = updateEvent.StartTime;
            editEvent.EndTime = updateEvent.EndTime;
            editEvent.Status = updateEvent.Status;
            editEvent.ModifiedDate = DateTime.Now;
            editEvent.IsDeleted = updateEvent.IsDeleted;
            editEvent.Amount = updateEvent.Amount;
            editEvent.AmountLimit = updateEvent.AmountLimit;

            // Lấy danh sách ảnh hiện tại
            var existingImages = await _imageRepository.GetByEventIdAsync(editEvent.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateEvent.ImgToDelete != null && updateEvent.ImgToDelete.Any())
            {
                foreach (var imageIdToDelete in updateEvent.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "EventImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateEvent.Img != null && updateEvent.Img.Any())
            {
                var newImageUrls = await _imageService.UploadEventImage(updateEvent.Img, editEvent.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        EventId = editEvent.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            // Lưu thông tin cập nhật
            await _eventRepository.UpdateAsync(editEvent);
            return editEvent;
        }

        public async Task<string> DonateEvent(int id, EventDonateDTO updateEvent)
        {          
            var editEvent = await _eventRepository.GetByIdAsync(id);
            if (editEvent == null)
            {
                throw new Exception($"Event with ID {id} not found!");
            }        
            var newTotalAmount = (editEvent.CurrentAmount ?? 0) + (updateEvent.Amount ?? 0);
            if (newTotalAmount > (editEvent.AmountLimit ?? 0))
            {
                throw new InvalidOperationException("Donation amount exceeds the allowed limit.");
            }          
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = updateEvent.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = updateEvent.Amount ?? 0,
                Description = $"Donation for Event: {editEvent.Name}",
                IsDeleted = false,
                Status = "Pending",
                EventId = id
            };
            var donation = await _donationService.CreateDonationPayment(donationDto);           
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (updateEvent.Amount.GetValueOrDefault() * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData(
            "vnp_OrderInfo",
            $"Thanh toán cho Donation {donation.Id}, eventId {id}, " +
            string.Join(", ", new[]
            {
            editEvent.FacilitiesWalletId != null ? $"walletId {editEvent.FacilitiesWalletId}" : null,
            editEvent.FoodStuffWalletId != null ? $"walletId {editEvent.FoodStuffWalletId}" : null,
            editEvent.NecessitiesWalletId != null ? $"walletId {editEvent.NecessitiesWalletId}" : null,
            editEvent.HealthWalletId != null ? $"walletId {editEvent.HealthWalletId}" : null
            }.Where(s => s != null))
            );
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            string uniqueTxnRef = $"{donation.Id}_{DateTime.Now.Ticks}";
            vnpay.AddRequestData("vnp_TxnRef", uniqueTxnRef);
            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);   
            var income = new Income
            {
                UserAccountId = updateEvent.UserAccountId,
                FacilitiesWalletId = editEvent.FacilitiesWalletId,
                FoodStuffWalletId = editEvent.FoodStuffWalletId,
                SystemWalletId = editEvent.SystemWalletId,
                HealthWalletId = editEvent.HealthWalletId, 
                NecessitiesWalletId = editEvent.NecessitiesWalletId,
                Amount = updateEvent.Amount ?? 0,
                Receiveday = DateTime.Now,
                Status = "Complete",
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = updateEvent.Amount ?? 0,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            return paymentUrl;
        }


        public async Task<Event> DeleteEvent(int id)
        {
            var even = await _eventRepository.GetByIdAsync(id);
            if (even == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            if (even.IsDeleted == true)
            {
                // Hard delete nếu đã bị soft delete
                await _eventRepository.RemoveAsync(even);
            }
            else
            {
                // Soft delete: đặt IsDeleted = true
                even.IsDeleted = true;
                await _eventRepository.UpdateAsync(even);
            }
            return even;
        }

        public async Task<Event> RestoreEvent(int id)
        {
            var even = await _eventRepository.GetByIdAsync(id);
            if (even == null)
            {
                throw new Exception($"Event with ID {id} not found");
            }

            if (even.IsDeleted == true) // Nếu đã bị soft delete
            {
                even.IsDeleted = false; // Khôi phục bằng cách đặt IsDeleted = false
                await _eventRepository.UpdateAsync(even);
            }
            return even;
        }
    }
}
