using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
using ChildrenVillageSOS_DAL.DTO.PaymentDTO;
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
    public class PaymentService  : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllAsync();
        }
        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task<Payment> CreatePayment(CreatePaymentDTO createPayment)
        {
            var newPayment = new Payment
            {
                PaymentMethod = createPayment.PaymentMethod,
                DonationId = createPayment.DonationId,
                Datetime = DateTime.Now,
                Amount = createPayment.Amount,
                Status = createPayment.Status,
            };
            await _paymentRepository.AddAsync(newPayment);
            return newPayment;
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
    }
}
