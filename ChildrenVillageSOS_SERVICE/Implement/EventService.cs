using ChildrenVillageSOS_DAL.DTO.AcademicReportDTO;
using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
using ChildrenVillageSOS_DAL.Enum;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class EventService : IEventService
    {
        private readonly IVillageRepository _villageRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IChildRepository _childRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IFacilitiesWalletRepository _failitiesWalletRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletRepository;
        private readonly IHealthWalletRepository _healthWalletRepository;
        private readonly IFoodStuffWalletRepository _foodStuffWalletRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IEventRepository _eventRepository;        
        private readonly IPaymentRepository _paymentRepository;       
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        private readonly IConfiguration _configuration;
        private readonly IDonationService _donationService;             
        private readonly IIncomeRepository _incomeRepository;
        public EventService(
            IHouseRepository houseRepository,
            IChildRepository childRepository,
            IVillageRepository villageRepository,
            IEventRepository eventRepository,
            INecessitiesWalletRepository necessitiesWalletRepository,
            IFoodStuffWalletRepository foodStuffWalletRepository,
            IHealthWalletRepository healthWalletRepository,
            IFacilitiesWalletRepository failitiesWalletRepository,
            IExpenseRepository expenseRepository,
            IUserAccountRepository userAccountRepository,
            IImageService imageService,
            IImageRepository imageRepository,          
            IPaymentRepository paymentRepository,          
            IConfiguration configuration,
            IDonationService donationService,           
            IIncomeRepository incomeRepository)
        {
            _houseRepository = houseRepository;
            _childRepository = childRepository;
            _villageRepository = villageRepository;
            _necessitiesWalletRepository = necessitiesWalletRepository;
            _foodStuffWalletRepository = foodStuffWalletRepository;
            _healthWalletRepository = healthWalletRepository;
            _failitiesWalletRepository = failitiesWalletRepository;
            _expenseRepository = expenseRepository;
            _eventRepository = eventRepository;
            _userAccountRepository = userAccountRepository;
            _eventRepository = eventRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;         
            _paymentRepository = paymentRepository;    
            _configuration = configuration;
            _donationService = donationService;         
            _incomeRepository = incomeRepository;

        }

       
        public async Task<EventAmountLimitDto> CalculateAmountLimitAsync(int eventId)
        {
            // Lấy thông tin sự kiện
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
            {
                throw new Exception($"Event with ID {eventId} not found.");
            }

            // Lấy villageId từ event
            string villageId = eventEntity.VillageId;

            // Lấy danh sách nhà thuộc làng
            var houses = await _houseRepository.GetHouseByVillageIdAsync(villageId);
            decimal totalHouseExpense = 0;
            var houseDetails = new List<HouseExpenseDto>();

            foreach (var house in houses)
            {
                // Lấy danh sách trẻ trong nhà
                var children = await _childRepository.GetChildByHouseIdAsync(house.Id);

                // Lọc trẻ có `HealthStatus` là `Bad`
                var badHealthChildren = children.Where(c => c.HealthStatus == "Bad" && c.ExpenseRequestStatus == "OnRequest");

                // Tính tổng chi phí của nhà
                decimal houseExpense = badHealthChildren.Sum(c => c.Amount ?? 0);

                houseDetails.Add(new HouseExpenseDto
                {
                    HouseId = house.Id,
                    HouseName = house.HouseName,
                    BadHealthChildrenCount = badHealthChildren.Count(),
                    HouseExpense = houseExpense
                });

                totalHouseExpense += houseExpense;
            }

          
            return new EventAmountLimitDto
            {
                EventId = eventId,
                AmountLimit = totalHouseExpense,
                Village = new VillageExpenseDto
                {
                    VillageId = villageId,
                    VillageName = eventEntity.Name,
                    TotalHouseExpense = totalHouseExpense,
                    Houses = houseDetails
                }
            };
        }
        public Task<EventResponseDTO[]> GetAllEventIsDeleteAsync()
        {
            return _eventRepository.GetAllEventIsDeleteAsync();
        }
        public Task<EventResponseDTO[]> GetAllEventArrayAsync()
        {
            return _eventRepository.GetAllEventArrayAsync();
        }
        public Task<EventResponseDTO[]> SearchEventArrayAsync(string searchTerm)
        {
            return _eventRepository.SearchEventArrayAsync(searchTerm);
        }

        public async Task<EventDetailsDTO> GetEventDetails(int eventId)
        {
            return await _eventRepository.GetEventDetails(eventId);
        }

        public async Task<IEnumerable<EventResponseDTO>> GetAllEvent()
        {
           
            var events = await _eventRepository.GetAllNotDeletedAsync();

            // Sử dụng Include để tải hình ảnh liên quan đến mỗi Event
            var eventResponseDTOs = events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                EventCode = e.EventCode,
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
        public async Task<Event> ApprovedEvent(CreateEventDTO createEvent, int villageExpenseId, string userId)
        {
            var allEventCodes = await _eventRepository.Entities()
                                                   .Select(c => c.EventCode)
                                                   .ToListAsync();
            string newEventCode = IdGenerator.GenerateId(allEventCodes, "E");
            //Retrieve villageExpense from the database

           var villageExpense = await _expenseRepository.GetByIdAsync(villageExpenseId);
            if (villageExpense == null || villageExpense.ExpenseType != "Special" || villageExpense.Status != "OnRequestToEvent")
            {
                throw new InvalidOperationException("Invalid village expense for creating an event.");
            }

            //Retrieve the village and verify the director
           var village = await _villageRepository.GetByIdAsync(villageExpense.VillageId);
            if (village == null || village.UserAccountId != userId)
            {
                throw new UnauthorizedAccessException($"Only the Director:{village.UserAccountId} can approve the event.");
            }

            decimal totalExpenseAmount = villageExpense.ExpenseAmount;

            // Check if all wallets have sufficient balance
            bool hasSufficientFunds = true;

            if (villageExpense.FacilitiesWalletId.HasValue)
            {
                var facilitiesWallet = await _failitiesWalletRepository.GetByIdAsync(villageExpense.FacilitiesWalletId.Value);
                if (facilitiesWallet == null || facilitiesWallet.Budget < totalExpenseAmount)
                {
                    hasSufficientFunds = false;
                }
            }

            if (villageExpense.FoodStuffWalletId.HasValue)
            {
                var foodStuffWallet = await _foodStuffWalletRepository.GetByIdAsync(villageExpense.FoodStuffWalletId.Value);
                if (foodStuffWallet == null || foodStuffWallet.Budget < totalExpenseAmount)
                {
                    hasSufficientFunds = false;
                }
            }

            if (villageExpense.HealthWalletId.HasValue)
            {
                var healthWallet = await _healthWalletRepository.GetByIdAsync(villageExpense.HealthWalletId.Value);
                if (healthWallet == null || healthWallet.Budget < totalExpenseAmount)
                {
                    hasSufficientFunds = false;
                }
            }

            if (villageExpense.NecessitiesWalletId.HasValue)
            {
                var necessitiesWallet = await _necessitiesWalletRepository.GetByIdAsync(villageExpense.NecessitiesWalletId.Value);
                if (necessitiesWallet == null || necessitiesWallet.Budget < totalExpenseAmount)
                {
                    hasSufficientFunds = false;
                }
            }

            // Update villageExpense status
            villageExpense.Status = ExpenseStatus.OnEvent.ToString();  // Update status for event creation
            villageExpense.ModifiedDate = DateTime.Now;
            await _expenseRepository.UpdateAsync(villageExpense);

            // Deduct amounts from wallets if funds are available
            if (villageExpense.FacilitiesWalletId.HasValue)
            {
                var facilitiesWallet = await _failitiesWalletRepository.GetByIdAsync(villageExpense.FacilitiesWalletId.Value);
                if (facilitiesWallet != null && facilitiesWallet.Budget >= totalExpenseAmount)
                {
                    facilitiesWallet.Budget -= totalExpenseAmount;
                    await _failitiesWalletRepository.UpdateAsync(facilitiesWallet);
                }
            }

            if (villageExpense.FoodStuffWalletId.HasValue)
            {
                var foodStuffWallet = await _foodStuffWalletRepository.GetByIdAsync(villageExpense.FoodStuffWalletId.Value);
                if (foodStuffWallet != null && foodStuffWallet.Budget >= totalExpenseAmount)
                {
                    foodStuffWallet.Budget -= totalExpenseAmount;
                    await _foodStuffWalletRepository.UpdateAsync(foodStuffWallet);
                }
            }

            if (villageExpense.HealthWalletId.HasValue)
            {
                var healthWallet = await _healthWalletRepository.GetByIdAsync(villageExpense.HealthWalletId.Value);
                if (healthWallet != null && healthWallet.Budget >= totalExpenseAmount)
                {
                    healthWallet.Budget -= totalExpenseAmount;
                    await _healthWalletRepository.UpdateAsync(healthWallet);
                }
            }

            if (villageExpense.NecessitiesWalletId.HasValue)
            {
                var necessitiesWallet = await _necessitiesWalletRepository.GetByIdAsync(villageExpense.NecessitiesWalletId.Value);
                if (necessitiesWallet != null && necessitiesWallet.Budget >= totalExpenseAmount)
                {
                    necessitiesWallet.Budget -= totalExpenseAmount;
                    await _necessitiesWalletRepository.UpdateAsync(necessitiesWallet);
                }
            }

            // Create the new event
            var newEvent = new Event
            {
                Name = createEvent.Name,
                Description = createEvent.Description,
                CreatedBy = createEvent.CreatedBy,
                FacilitiesWalletId = villageExpense.FacilitiesWalletId,
                FoodStuffWalletId = villageExpense.FoodStuffWalletId,
                SystemWalletId = villageExpense.SystemWalletId,
                HealthWalletId = villageExpense.HealthWalletId,
                NecessitiesWalletId = villageExpense.NecessitiesWalletId,
                StartTime = createEvent.StartTime,
                EndTime = createEvent.EndTime,
                EventCode = newEventCode,
                Status = SystemStatus.Active.ToString(),
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Amount = 0,
                CurrentAmount = 0,
                AmountLimit = villageExpense.ExpenseAmount,
                VillageId = villageExpense.VillageId,
            };

            await _eventRepository.AddAsync(newEvent);

            // Upload event images
            List<string> imageUrls = await _imageService.UploadEventImage(createEvent.Img, newEvent.Id);
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

            // Update villageExpense status
            villageExpense.EventId = newEvent.Id;
            villageExpense.Status = ExpenseStatus.Approved.ToString();
            villageExpense.ModifiedDate = DateTime.Now;
            villageExpense.ApprovedBy = createEvent.CreatedBy;
            await _expenseRepository.UpdateAsync(villageExpense);

            return newEvent;
        }

        


        public async Task<EventResponseDTO> GetEventById(int id)
        {
            return _eventRepository.GetEventById(id);
        }

        public async Task<Event> CreateEvent(EventCreateDTO createEvent)
        {
            var allEventCodes = await _eventRepository.Entities()
                                       .Select(c => c.EventCode)
                                       .ToListAsync();
            string newEventCode = IdGenerator.GenerateId(allEventCodes, "E");

            var newEvent = new Event
            {
                CreatedBy = createEvent.CreatedBy,
                Name = createEvent.Name,
                Description = createEvent.Description,
                FacilitiesWalletId = createEvent.FacilitiesWalletId,
                FoodStuffWalletId = createEvent.FoodStuffWalletId,
                SystemWalletId = createEvent.SystemWalletId,
                HealthWalletId = createEvent.HealthWalletId,
                NecessitiesWalletId = createEvent.NecessitiesWalletId,
                StartTime = createEvent.StartTime,
                EndTime = createEvent.EndTime,
                EventCode = newEventCode,
                Status = "Active",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Amount = 0,
                CurrentAmount = 0,
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

            editEvent.CreatedBy = updateEvent.CreatedBy;
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
            //editEvent.EventCode = editEvent.EventCode;
            editEvent.ModifiedDate = DateTime.Now;
            editEvent.IsDeleted = updateEvent.IsDeleted;
            //editEvent.Amount = updateEvent.Amount;
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

            
            if (editEvent.Status == "Close")
            {
                throw new InvalidOperationException("Donations are not allowed for closed events.");
            }

            var newTotalAmount = (editEvent.CurrentAmount ?? 0) + (updateEvent.Amount ?? 0);
            if (newTotalAmount > (editEvent.AmountLimit ?? 0))
            {
                throw new InvalidOperationException("Donation amount exceeds the allowed limit.");
            }

            // Fetch user information
            string? userName, userEmail, address;
            string? phone;

            if (!string.IsNullOrEmpty(updateEvent.UserAccountId))
            {
                var user = await _userAccountRepository.GetByIdAsync(updateEvent.UserAccountId);
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
                userName = updateEvent.UserName;
                userEmail = updateEvent.UserEmail;
                phone = updateEvent.Phone;
                address = updateEvent.Address;
            }

            // Proceed with donation creation
            var donationDto = new DonateDTO
            {
                FoodStuffWalletId = editEvent.FoodStuffWalletId,
                FacilitiesWalletId = editEvent.FacilitiesWalletId,
                NecessitiesWalletId = editEvent.NecessitiesWalletId,
                HealthWalletId = editEvent.HealthWalletId,
                UserAccountId = updateEvent.UserAccountId,
                EventCode = editEvent.EventCode,
                EventId = editEvent.Id,
                UserName = userName,
                UserEmail = userEmail,
                Phone = phone,
                Address = address,
                DonationType = DonateType.Event.ToString(),
                DateTime = DateTime.Now,
                Amount = updateEvent.Amount ?? 0,
                Description = updateEvent.Description,
                IsDeleted = false,
                Status = DonateStatus.Pending.ToString(),
            };

            var donation = await _donationService.DonateNow(donationDto);
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
                Status = IncomeStatus.Pending.ToString(),
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = updateEvent.Amount ?? 0,
                PaymentMethod = PaymentMethod.Banking.ToString(),
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = DonateStatus.Pending.ToString(),
            };
            await _paymentRepository.AddAsync(payment);

            return paymentUrl;
        }

        public async Task<Event> CloseEvent(int eventId)
        {
            // Lấy Event từ cơ sở dữ liệu
            var eventToClose = await _eventRepository.GetByIdAsync(eventId);
            if (eventToClose == null || eventToClose.Status != "Active")
            {
                throw new InvalidOperationException("Event not found or is not active.");
            }

            // Lấy Expense liên quan đến Event có Status = "OnEvent" hoặc "Approved"
            var relatedExpense = await _expenseRepository.GetExpenseByEventAndStatusAsync(eventToClose.VillageId, eventId, new[] { "OnEvent", "Approved" });
            if (relatedExpense == null)
            {
                throw new InvalidOperationException("Related expense for the event not found.");
            }

            // Gán CurrentAmount từ Event vào AmountReceive của Expense
            relatedExpense.AmountReceive = eventToClose.CurrentAmount ?? 0;

            // Nếu trạng thái của Expense là "OnEvent", thực hiện trừ tiền từ các ví
            if (relatedExpense.Status == "OnEvent")
            {
                if (eventToClose.FacilitiesWalletId.HasValue)
                {
                    var facilitiesWallet = await _failitiesWalletRepository.GetByIdAsync(eventToClose.FacilitiesWalletId.Value);
                    if (facilitiesWallet != null)
                    {
                        facilitiesWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                        await _failitiesWalletRepository.UpdateAsync(facilitiesWallet);
                    }
                }

                if (eventToClose.FoodStuffWalletId.HasValue)
                {
                    var foodStuffWallet = await _foodStuffWalletRepository.GetByIdAsync(eventToClose.FoodStuffWalletId.Value);
                    if (foodStuffWallet != null)
                    {
                        foodStuffWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                        await _foodStuffWalletRepository.UpdateAsync(foodStuffWallet);
                    }
                }

                if (eventToClose.HealthWalletId.HasValue)
                {
                    var healthWallet = await _healthWalletRepository.GetByIdAsync(eventToClose.HealthWalletId.Value);
                    if (healthWallet != null)
                    {
                        healthWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                        await _healthWalletRepository.UpdateAsync(healthWallet);
                    }
                }

                if (eventToClose.NecessitiesWalletId.HasValue)
                {
                    var necessitiesWallet = await _necessitiesWalletRepository.GetByIdAsync(eventToClose.NecessitiesWalletId.Value);
                    if (necessitiesWallet != null)
                    {
                        necessitiesWallet.Budget -= relatedExpense.AmountReceive ?? 0;
                        await _necessitiesWalletRepository.UpdateAsync(necessitiesWallet);
                    }
                }

                
            }

            // Cập nhật trạng thái của Event thành "Close"
            eventToClose.Status = SystemStatus.Close.ToString();
            eventToClose.ModifiedDate = DateTime.Now;

            // Cập nhật Expense
            relatedExpense.Status = ExpenseStatus.Approved.ToString();
            relatedExpense.ModifiedDate = DateTime.Now;

            await _expenseRepository.UpdateAsync(relatedExpense);
            await _eventRepository.UpdateAsync(eventToClose);

            return eventToClose;
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

        public async Task<List<Event>> SearchEvents(SearchEventDTO searchEventDTO)
        {
            return await _eventRepository.SearchEvents(searchEventDTO);
        }
    }
}
