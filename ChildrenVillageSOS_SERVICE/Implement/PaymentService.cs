using ChildrenVillageSOS_DAL.DTO.DonationDTO;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class PaymentService  : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IDonationService _donationService;
        private readonly IDonationRepository _donationRepository;
        private readonly IFacilitiesWalletRepository _failitiesWalletRepository;
        private readonly IFoodStuffWalletRepository _foodStuffWalletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHealthWalletRepository _healthWalletRepository;
        private readonly ISystemWalletRepository _systemWalletRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IConfiguration _configuration;
        public PaymentService(IPaymentRepository paymentRepository,IDonationService donationService, IConfiguration configuration, IDonationRepository donationRepository, IFacilitiesWalletRepository failitiesWalletRepository, ITransactionRepository transactionRepository, IFoodStuffWalletRepository foodStuffWalletRepository, IHealthWalletRepository healthWalletRepository, ISystemWalletRepository systemWalletRepository, INecessitiesWalletRepository necessitiesWalletRepository, IIncomeRepository incomeRepository)
        {
            _paymentRepository = paymentRepository;
            _donationService = donationService;
            _configuration = configuration;
            _donationRepository = donationRepository;
            _failitiesWalletRepository = failitiesWalletRepository;
            _transactionRepository = transactionRepository;
            _foodStuffWalletRepository = foodStuffWalletRepository;
            _healthWalletRepository = healthWalletRepository;
            _systemWalletRepository = systemWalletRepository;
            _necessitiesWalletRepository = necessitiesWalletRepository;
            _incomeRepository = incomeRepository;   
        }
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllAsync();
        }
        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task<string> CreatePayment(PaymentRequest paymentRequest)
        {
            // Tạo donation trước khi xử lý payment
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);

            // Sau khi tạo donation, dùng DonationId để tạo payment
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Số tiền nhân 100 vì VNPay yêu cầu
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Tạo bản ghi Payment sau khi tạo URL
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };

            // Lưu payment vào cơ sở dữ liệu
            await _paymentRepository.AddAsync(payment);

            // Trả về URL để redirect người dùng đến VNPay
            return paymentUrl;
        }

        public async Task<string> CreateFacilitiesWalletPayment(PaymentRequest paymentRequest)
        {
            // Step 1: Create Donation
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);
            var facilitiesWallet = await _failitiesWalletRepository.GetFacilitiesWalletByUserIdAsync("UA001");
            // Step 2: Create VNPay URL
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Multiply by 100 for VNPay
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}, facilitiesWalletId {facilitiesWallet.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Step 3: Update FacilitiesWallet Budget
           
            //if (facilitiesWallet != null)
            //{
            //    facilitiesWallet.Budget += paymentRequest.Amount;
            //    await _failitiesWalletRepository.UpdateAsync(facilitiesWallet);
            //}

            // Step 4: Create Transaction
            var income = new Income
            {
                FacilitiesWalletId = facilitiesWallet?.Id,
                Amount = paymentRequest.Amount,
                Receiveday = DateTime.Now,
                UserAccountId = paymentRequest.UserAccountId,
                Status = "Completed",
                CreatedDate = DateTime.Now,
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 5: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            // Return the VNPay URL for the user to complete the payment
            return paymentUrl;
        }
        public async Task<string> CreateFoodStuffWalletPayment(PaymentRequest paymentRequest)
        {
            // Step 1: Create Donation
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);
            var foodWallet = await _foodStuffWalletRepository.GetWalletByUserIdAsync("UA001");
            // Step 2: Create VNPay URL
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Multiply by 100 for VNPay
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}, foodStuffWalletId {foodWallet.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Step 3: Update FacilitiesWallet Budget
            
            //if (foodWallet != null)
            //{
            //    foodWallet.Budget += paymentRequest.Amount;
            //    await _foodStuffWalletRepository.UpdateAsync(foodWallet);
            //}

            // Step 4: Create Transaction
            var income = new Income
            {
                FacilitiesWalletId = foodWallet?.Id,
                Amount = paymentRequest.Amount,
                Receiveday = DateTime.Now,
                UserAccountId = paymentRequest.UserAccountId,
                Status = "Completed",
                CreatedDate = DateTime.Now,
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 5: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            // Return the VNPay URL for the user to complete the payment
            return paymentUrl;
        }
        public async Task<Payment> UpdatePayment(int id, UpdatePaymentDTO updatePayment)
        {
            var updaPayment = await _paymentRepository.GetByIdAsync(id);
            if (updaPayment == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updaPayment.DonationId = updatePayment.DonationId;

            updaPayment.PaymentMethod = updatePayment.PaymentMethod;
            updaPayment.Amount = updatePayment.Amount;
            updaPayment.Status = updatePayment.Status;    
            await _paymentRepository.UpdateAsync(updaPayment);
            return updaPayment;

        }
        public async Task<Payment> DeletePayment(int id)
        {
            var pay = await _paymentRepository.GetByIdAsync(id);
            if (pay == null)
            {
                throw new Exception($"Payment with ID{id} not found");
            }
            await _paymentRepository.RemoveAsync(pay);
            return pay;
        }
        public async Task<Payment> GetPaymentByDonationIdAsync(int donationId)
        {
            return await _paymentRepository.GetPaymentByDonationIdAsync(donationId);
        }

        public async Task<Payment> SoftDelete(int id)
        {
            var updaPayment = await _paymentRepository.GetByIdAsync(id);
            if (updaPayment == null)
            {
                throw new Exception($"Payment with ID{id} not found!");
            }          
            updaPayment.IsDeleted = true;
            await _paymentRepository.UpdateAsync(updaPayment);
            return updaPayment;

        }

        public async Task<string> CreateHealthWalletPayment(PaymentRequest paymentRequest)
        {
            // Step 1: Create Donation
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);

            // Step 2: Create VNPay URL
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];
            var wallet = await _healthWalletRepository.GetHealthWalletByUserIdAsync("UA001");
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Multiply by 100 for VNPay
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}, healthWalletId {wallet.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Step 3: Update FacilitiesWallet Budget
            
            //if (wallet != null)
            //{
            //    wallet.Budget += paymentRequest.Amount;
            //    await _healthWalletRepository.UpdateAsync(wallet);
            //}

            // Step 4: Create Transaction
            var income = new Income
            {
                FacilitiesWalletId = wallet?.Id,
                Amount = paymentRequest.Amount,
                Receiveday = DateTime.Now,
                UserAccountId = paymentRequest.UserAccountId,
                Status = "Completed",
                CreatedDate = DateTime.Now,
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 5: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            // Return the VNPay URL for the user to complete the payment
            return paymentUrl;
        }
        public async Task<string> CreateSystemWalletPayment(PaymentRequest paymentRequest)
        {
            // Step 1: Create Donation
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);
            var wallet = await _systemWalletRepository.GetWalletByUserIdAsync("UA001");
            // Step 2: Create VNPay URL
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Multiply by 100 for VNPay
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}, systemWalletId {wallet.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Step 3: Update FacilitiesWallet Budget
           
            //if (wallet != null)
            //{
            //    wallet.Budget += paymentRequest.Amount;
            //    await _systemWalletRepository.UpdateAsync(wallet);
            //}

            // Step 4: Create Income
            var income = new Income
            {
                FacilitiesWalletId = wallet?.Id,
                Amount = paymentRequest.Amount,
                Receiveday = DateTime.Now,
                UserAccountId = paymentRequest.UserAccountId,
                Status = "Completed",
                CreatedDate = DateTime.Now,
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 5: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            // Return the VNPay URL for the user to complete the payment
            return paymentUrl;
        }
        public async Task<string> CreateNecesstiesWalletPayment(PaymentRequest paymentRequest)
        {
            // Step 1: Create Donation
            var donationDto = new CreateDonationPayment
            {
                UserAccountId = paymentRequest.UserAccountId,
                DonationType = "Online",
                DateTime = DateTime.Now,
                Amount = paymentRequest.Amount,
                Description = "Donation for SOS Children's Village",
                IsDeleted = false,
                Status = "Pending"
            };

            var donation = await _donationService.CreateDonationPayment(donationDto);
            var wallet = await _necessitiesWalletRepository.GetNecessitiesWalletByUserIdAsync("UA001");
            // Step 2: Create VNPay URL
            var vnp_ReturnUrl = _configuration["VNPay:ReturnUrl"];
            var vnp_Url = _configuration["VNPay:Url"];
            var vnp_TmnCode = _configuration["VNPay:TmnCode"];
            var vnp_HashSecret = _configuration["VNPay:HashSecret"];

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Multiply by 100 for VNPay
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.105");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {donation.Id}, necessitiesWalletId {wallet.Id}");
            vnpay.AddRequestData("vnp_OrderType", "donation");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_ReturnUrl);
            vnpay.AddRequestData("vnp_TxnRef", donation.Id.ToString());
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            // Step 3: Update FacilitiesWallet Budget
           
            //if (wallet != null)
            //{
            //    wallet.Budget += paymentRequest.Amount;
            //    await _necessitiesWalletRepository.UpdateAsync(wallet);
            //}

            // Step 4: Create Transaction
            var income = new Income
            {
                FacilitiesWalletId = wallet?.Id,
                Amount = paymentRequest.Amount,
                Receiveday = DateTime.Now,
                UserAccountId = paymentRequest.UserAccountId,
                Status = "Completed",
                CreatedDate = DateTime.Now,
                DonationId = donation.Id
            };
            await _incomeRepository.AddAsync(income);

            // Step 5: Create Payment
            var payment = new Payment
            {
                DonationId = donation.Id,
                Amount = paymentRequest.Amount,
                PaymentMethod = "Banking",
                DateTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Status = "Pending"
            };
            await _paymentRepository.AddAsync(payment);

            // Return the VNPay URL for the user to complete the payment
            return paymentUrl;
        }
    }
}
