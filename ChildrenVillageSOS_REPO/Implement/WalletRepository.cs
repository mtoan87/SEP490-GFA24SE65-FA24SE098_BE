using ChildrenVillageSOS_REPO.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Implement
{
    public class WalletRepository : IWalletRepository
    {

        private readonly IFacilitiesWalletRepository _failitiesWalletRepository;
        private readonly IFoodStuffWalletRepository _foodStuffWalletRepository;
        private readonly INecessitiesWalletRepository _necessitiesWalletRepository;
        private readonly ISystemWalletRepository _systemWalletRepository;
        private readonly IHealthWalletRepository _healthWalletRepository;
        private readonly IIncomeRepository _incomeRepository;
        public WalletRepository(IFacilitiesWalletRepository facilitiesWalletRepository, ISystemWalletRepository systemWalletRepository, INecessitiesWalletRepository necessitiesWalletRepository, IFoodStuffWalletRepository foodStuffWalletRepository, IHealthWalletRepository healthWalletRepository, IIncomeRepository incomeRepository)
        {

            _failitiesWalletRepository = facilitiesWalletRepository;
            _systemWalletRepository = systemWalletRepository;
            _foodStuffWalletRepository = foodStuffWalletRepository;
            _necessitiesWalletRepository = necessitiesWalletRepository;
            _healthWalletRepository = healthWalletRepository;
            _incomeRepository = incomeRepository;

        }
        
    
        
        public async Task UpdateHealthWalletBudget(int walletId, decimal amount)
        {
            var wallet = await _healthWalletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Budget += amount;
                await _healthWalletRepository.UpdateAsync(wallet);
            }
        }

        public async Task UpdateNecessitiesWalletBudget(int walletId, decimal amount)
        {
            var wallet = await _necessitiesWalletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Budget += amount;
                await _necessitiesWalletRepository.UpdateAsync(wallet);
            }
        }

        public async Task UpdateFacilitiesWalletBudget(int walletId, decimal amount)
        {
            var wallet = await _failitiesWalletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Budget += amount;
                await _failitiesWalletRepository.UpdateAsync(wallet);
            }
        }

        public async Task UpdateFoodStuffWalletBudget(int walletId, decimal amount)
        {
            var wallet = await _foodStuffWalletRepository.GetByIdAsync(walletId);
            if (wallet != null)
            {
                wallet.Budget += amount;
                await _foodStuffWalletRepository.UpdateAsync(wallet);
            }
        }
    }
}
