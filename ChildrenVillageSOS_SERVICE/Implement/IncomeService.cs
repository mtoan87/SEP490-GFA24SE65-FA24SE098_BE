using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.DTO.IncomeDTO;
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
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        public IncomeService(IIncomeRepository incomeRepository) 
        { 
            _incomeRepository = incomeRepository;   
        }
        public async Task<IEnumerable<Income>> GetAllIncomes()
        {
            return await _incomeRepository.GetAllAsync();
        }
        public async Task<Income> GetIncomeById(int id)
        {
            return await _incomeRepository.GetByIdAsync(id);
        }
        public async Task<Income> CreateIncome(CreateIncomeDTO createIncome)
        {
            var newExpense = new Income
            {
                DonationId = createIncome.DonationId,
                UserAccountId = createIncome.UserAccountId,
                Receiveday = DateTime.Now,
                IsDeleted = createIncome.IsDeleted,
            };
            await _incomeRepository.AddAsync(newExpense);
            return newExpense;
        }
        public async Task<Income> UpdateIncome(int id, UpdateIncomeDTO updateIncome)
        {
            var updIncome = await _incomeRepository.GetByIdAsync(id);
            if (updIncome == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updIncome.UserAccountId = updateIncome.UserAccountId;

            updIncome.IsDeleted = updateIncome.IsDeleted;
            await _incomeRepository.UpdateAsync(updIncome);
            return updIncome;

        }
        public async Task<Income> DeleteIncome(int id)
        {
            var inc = await _incomeRepository.GetByIdAsync(id);
            if (inc == null)
            {
                throw new Exception($"Income with ID{id} not found");
            }
            await _incomeRepository.RemoveAsync(inc);
            return inc;
        }
    }
}
