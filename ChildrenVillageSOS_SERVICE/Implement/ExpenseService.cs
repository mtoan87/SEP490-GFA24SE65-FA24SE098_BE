using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
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
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly INecessitiesWalletService _necessitiesWalletService;
        private readonly IHealthWalletService _healthWalletService;
        private readonly IFacilitiesWalletService _facilitiesWalletService;
        private readonly IFoodStuffWalletService _foodStuffWalletService;   
        public ExpenseService(IExpenseRepository expenseRepository,
            INecessitiesWalletService necessitiesWalletService,
            IHealthWalletService  healthWalletService,
            IFoodStuffWalletService foodStuffWalletService,
            IFacilitiesWalletService facilitiesWalletService )
        {
            _expenseRepository = expenseRepository;
            _necessitiesWalletService = necessitiesWalletService;
            _healthWalletService = healthWalletService;
            _facilitiesWalletService = facilitiesWalletService;
            _foodStuffWalletService = foodStuffWalletService;
            
        }

        public ExpenseResponseDTO[] GetFormatedExpenses()
        {
            return _expenseRepository.GetAllExpenses();
        }
        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await _expenseRepository.GetAllAsync();
        }
        public async Task<Expense> GetExpenseById(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }
        public async Task<Expense> UpdateExpense(int id, UpdateExpenseDTO updateExpense)
        {
            var updExpense = await _expenseRepository.GetByIdAsync(id);
            if (updExpense == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updExpense.ExpenseAmount = updateExpense.ExpenseAmount;
            updExpense.Description = updateExpense.Description;
            updExpense.HouseId = updateExpense.HouseId;
            updExpense.IsDeleted = updateExpense.IsDeleted;
            await _expenseRepository.UpdateAsync(updExpense);
            return updExpense;

        }
        public async Task<Expense> CreateExpense(CreateExepenseDTO createExepense)
        {
            var newExpense = new Expense
            {
                ExpenseAmount = createExepense.ExpenseAmount,
                Description = createExepense.Description,
                Expenseday = DateTime.Now,
                CreatedDate = DateTime.Now,
                Status = "Pending",
                HouseId = createExepense.HouseId,
                IsDeleted = false,
                FacilitiesWalletId = createExepense.FacilitiesWalletId,
                FoodStuffWalletId = createExepense.FoodStuffWalletId,
                HealthWalletId = createExepense.HealthWalletId,
                NecessitiesWalletId = createExepense.NecessitiesWalletId
            };
                     
            await _expenseRepository.AddAsync(newExpense);                     
            return newExpense;
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
        public async Task<Expense> SoftDelete(int id)
        {
            var exp = await _expenseRepository.GetByIdAsync(id);
            if (exp == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            exp.IsDeleted = true;
            await _expenseRepository.UpdateAsync(exp);
            return exp;
        }

        public async Task<Expense> ConfirmExpense(int id)
        {
            var exp = await _expenseRepository.GetByIdAsync(id);
            if (exp == null)
            {
                throw new Exception($"Expense with ID{id} is not found");
            }
            exp.Status = "Approved";
            exp.ModifiedDate = DateTime.Now;
            await _expenseRepository.UpdateAsync(exp);
            if (exp.FacilitiesWalletId.HasValue)
            {
                var facilitiesWallet = await _facilitiesWalletService.GetFacilitiesWalletById(exp.FacilitiesWalletId.Value);
                if (facilitiesWallet != null)
                {
                    facilitiesWallet.Budget -= exp.ExpenseAmount;
                    if (facilitiesWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Facilities Wallet.");
                }
            }


            if (exp.FoodStuffWalletId.HasValue)
            {
                var foodStuffWallet = await _foodStuffWalletService.GetFoodWalletById(exp.FoodStuffWalletId.Value);
                if (foodStuffWallet != null)
                {
                    foodStuffWallet.Budget -= exp.ExpenseAmount;
                    if (foodStuffWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Food Stuff Wallet.");
                }
            }
            if (exp.HealthWalletId.HasValue)
            {
                var healthWallet = await _healthWalletService.GetHealthWalletById(exp.HealthWalletId.Value);
                if (healthWallet != null)
                {
                    healthWallet.Budget -= exp.ExpenseAmount;
                    if (healthWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Health Wallet.");
                }
            }
            if (exp.NecessitiesWalletId.HasValue)
            {
                var necessitiesWallet = await _necessitiesWalletService.GetNecessitiesWalletById(exp.NecessitiesWalletId.Value);
                if (necessitiesWallet != null)
                {
                    necessitiesWallet.Budget -= exp.ExpenseAmount;
                    if (necessitiesWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Necessities Wallet.");
                }
            }           
            return exp;
        }
    }
}
