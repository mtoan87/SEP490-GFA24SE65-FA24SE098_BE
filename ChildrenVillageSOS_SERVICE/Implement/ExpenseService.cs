using ChildrenVillageSOS_DAL.DTO.ExpenseDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletService;
        private readonly IHealthWalletRepository _healthWalletService;
        private readonly IFacilitiesWalletRepository _facilitiesWalletService;
        private readonly IFoodStuffWalletRepository _foodStuffWalletService;   
        private readonly ISystemWalletRepository _systemWalletService;
        public ExpenseService(IExpenseRepository expenseRepository,
            INecessitiesWalletRepository necessitiesWalletService,
            IHealthWalletRepository healthWalletService,
            IFoodStuffWalletRepository foodStuffWalletService,
            IFacilitiesWalletRepository facilitiesWalletService,
            IWalletRepository walletRepository,
            ISystemWalletRepository systemWalletService)
        {
            _expenseRepository = expenseRepository;
            _necessitiesWalletService = necessitiesWalletService;
            _healthWalletService = healthWalletService;
            _facilitiesWalletService = facilitiesWalletService;
            _foodStuffWalletService = foodStuffWalletService;          
            _systemWalletService = systemWalletService;
        }

        public DataTable getExpense()
        {
            return _expenseRepository.getExpense();
        }
        public ExpenseResponseDTO[] GetFormatedExpenses()
        {
            return _expenseRepository.GetAllExpenses();
        }
        public Expense[] GetExpenseByFacilitiesWalletId(int id)
        {
            return _expenseRepository.GetExpenseByFacilitiesWalletId(id);
        }
        public Expense[] GetExpenseByFoodWalletId(int id)
        {
            return _expenseRepository.GetExpenseByFoodWalletId(id);
        }
        public Expense[] GetExpenseByHealthWalletId(int id)
        {
            return _expenseRepository.GetExpenseByHealthWalletId(id);
        }
        public Expense[] GetExpenseByNesceWalletId(int id)
        {
            return _expenseRepository.GetExpenseByNecessilitiesWalletId(id);
        }
        public Expense[] GetExpenseBySysWalletId(int id)
        {
            return _expenseRepository.GetExpenseBySystemWalletId(id);
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
                throw new Exception($"Expense with ID {id} not found!");
            }

            decimal oldExpenseAmount = updExpense.ExpenseAmount;
            updExpense.ExpenseAmount = updateExpense.ExpenseAmount;
            updExpense.Description = updateExpense.Description;
            updExpense.ModifiedDate = DateTime.Now;
            updExpense.HouseId = updateExpense.HouseId;

            // Determine which wallet to update based on the expense's wallet
            if (updExpense.FacilitiesWalletId.HasValue)
            {
                var wallet = await _facilitiesWalletService.GetByIdAsync(updExpense.FacilitiesWalletId.Value);
                if (wallet != null)
                {
                    // Adjust the wallet's balance (subtract old expense, add new expense)
                    wallet.Budget += oldExpenseAmount;  // Revert the old expense
                    wallet.Budget -= updateExpense.ExpenseAmount;  // Subtract the new expense
                    await _facilitiesWalletService.UpdateAsync(wallet);
                }
            }

            // Repeat for other wallets like FoodStuffWallet, HealthWallet, etc.
            if (updExpense.FoodStuffWalletId.HasValue)
            {
                var wallet = await _foodStuffWalletService.GetByIdAsync(updExpense.FoodStuffWalletId.Value);
                if (wallet != null)
                {
                    wallet.Budget += oldExpenseAmount;
                    wallet.Budget -= updateExpense.ExpenseAmount;
                    await _foodStuffWalletService.UpdateAsync(wallet);
                }
            }

            if (updExpense.HealthWalletId.HasValue)
            {
                var wallet = await _healthWalletService.GetByIdAsync(updExpense.HealthWalletId.Value);
                if (wallet != null)
                {
                    wallet.Budget += oldExpenseAmount;
                    wallet.Budget -= updateExpense.ExpenseAmount;
                    await _healthWalletService.UpdateAsync(wallet);
                }
            }

            if (updExpense.SystemWalletId.HasValue)
            {
                var wallet = await _systemWalletService.GetByIdAsync(updExpense.SystemWalletId.Value);
                if (wallet != null)
                {
                    wallet.Budget += oldExpenseAmount;
                    wallet.Budget -= updateExpense.ExpenseAmount;
                    await _systemWalletService.UpdateAsync(wallet);
                }
            }

            if (updExpense.NecessitiesWalletId.HasValue)
            {
                var wallet = await _necessitiesWalletService.GetByIdAsync(updExpense.NecessitiesWalletId.Value);
                if (wallet != null)
                {
                    wallet.Budget += oldExpenseAmount;
                    wallet.Budget -= updateExpense.ExpenseAmount;
                    await _necessitiesWalletService.UpdateAsync(wallet);
                }
            }

            // Update the Expense record
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
                SystemWalletId = createExepense.SystemWalletId,
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
                var facilitiesWallet = await _facilitiesWalletService.GetByIdAsync(exp.FacilitiesWalletId.Value);
                if (facilitiesWallet != null)
                {
                    facilitiesWallet.Budget -= exp.ExpenseAmount;
                    if (facilitiesWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Facilities Wallet.");
                }
            }


            if (exp.FoodStuffWalletId.HasValue)
            {
                var foodStuffWallet = await _foodStuffWalletService.GetByIdAsync(exp.FoodStuffWalletId.Value);
                if (foodStuffWallet != null)
                {
                    foodStuffWallet.Budget -= exp.ExpenseAmount;
                    if (foodStuffWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Food Stuff Wallet.");
                }
            }
            if (exp.HealthWalletId.HasValue)
            {
                var healthWallet = await _healthWalletService.GetByIdAsync(exp.HealthWalletId.Value);
                if (healthWallet != null)
                {
                    healthWallet.Budget -= exp.ExpenseAmount;
                    if (healthWallet.Budget < 0)
                        throw new InvalidOperationException("Insufficient budget in Health Wallet.");
                }
            }
            if (exp.NecessitiesWalletId.HasValue)
            {
                var necessitiesWallet = await _necessitiesWalletService.GetByIdAsync(exp.NecessitiesWalletId.Value);
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
