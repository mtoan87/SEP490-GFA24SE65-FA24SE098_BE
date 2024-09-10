using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _expenseRepository.GetAllAsync();
        }
        public async Task<Expense> GetExpenseById(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }
        public async Task<Expense> CreateExpense(CreateExepenseDTO createExepense) 
        {
            var newExpense = new Expense
            {
                ExpenseAmount = createExepense.ExpenseAmount,
                Description = createExepense.Description,
                Expenseday = DateTime.Now,
                HouseId = createExepense.HouseId,
                IsDelete = createExepense.IsDelete,
            };
            await _expenseRepository.AddAsync(newExpense);
            return newExpense;
        }
        public async Task<Expense> UpdateExpense(int id, UpdateExpenseDTO updateExpense)
        {
            var updExpense = await _expenseRepository.GetByIdAsync(id);
            if (updExpense == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updateExpense.ExpenseAmount = updExpense.ExpenseAmount;
            updateExpense.Description = updExpense.Description;
            updateExpense.HouseId = updExpense.HouseId;
            updateExpense.IsDelete = updExpense.IsDelete;
            await _expenseRepository.UpdateAsync(updExpense);
            return updExpense;

        }
        public async Task<Expense> DeleteExpense(int id)
        {
            var exp = await _expenseRepository.GetByIdAsync(id);
            if(exp == null)
            {
                throw new Exception($"Expense with ID{id} not found");
            }
            await _expenseRepository.RemoveAsync(exp);
            return exp;
        }
    }
}
