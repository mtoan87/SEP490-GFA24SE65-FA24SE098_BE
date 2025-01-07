using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.DTO.VillageDTO;
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
    public class ChildProgressService : IChildProgressService
    {
        private readonly IChildProgressRepository _childProgressRepository;

        public ChildProgressService(IChildProgressRepository childProgressRepository)
        {
            _childProgressRepository = childProgressRepository;
        }

        public async Task<IEnumerable<ChildProgress>> GetAllChildProgresses()
        {
            return await _childProgressRepository.GetAllNotDeletedAsync();
        }

        public async Task<ChildProgress> GetChildProgressById(int id)
        {
            return await _childProgressRepository.GetByIdAsync(id);
        }

        public async Task<ChildProgress> CreateChildProgress(CreateChildProgressDTO createChildProgress)
        {
            var newChildProgress = new ChildProgress
            {
                ChildId = createChildProgress.ChildId,
                Description = createChildProgress.Description,
                Date = createChildProgress.Date ?? DateTime.Now,
                Category = createChildProgress.Category,
                EventId = createChildProgress.EventId,
                ActivityId = createChildProgress.ActivityId,
                CreatedBy = createChildProgress.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _childProgressRepository.AddAsync(newChildProgress);
            return newChildProgress;
        }

        public async Task<ChildProgress> UpdateChildProgress(int id, UpdateChildProgressDTO updateChildProgress)
        {
            var existingChildProgress = await _childProgressRepository.GetByIdAsync(id);
            if (existingChildProgress == null)
            {
                throw new Exception($"ChildProgress with ID {id} not found!");
            }

            existingChildProgress.ChildId = updateChildProgress.ChildId;
            existingChildProgress.Description = updateChildProgress.Description;
            existingChildProgress.Date = updateChildProgress.Date ?? DateTime.Now;
            existingChildProgress.Category = updateChildProgress.Category;
            existingChildProgress.EventId = updateChildProgress.EventId;
            existingChildProgress.ActivityId = updateChildProgress.ActivityId;
            existingChildProgress.ModifiedBy = updateChildProgress.ModifiedBy;
            existingChildProgress.ModifiedDate = DateTime.Now;

            await _childProgressRepository.UpdateAsync(existingChildProgress);
            return existingChildProgress;
        }

        public async Task<ChildProgress> DeleteChildProgress(int id)
        {
            var childProgress = await _childProgressRepository.GetByIdAsync(id);
            if (childProgress == null)
            {
                throw new Exception($"ChildProgress with ID {id} not found");
            }

            if (childProgress.IsDeleted)
            {
                await _childProgressRepository.RemoveAsync(childProgress);
            }
            else
            {
                childProgress.IsDeleted = true;
                await _childProgressRepository.UpdateAsync(childProgress);
            }

            return childProgress;
        }

        public async Task<ChildProgress> RestoreChildProgress(int id)
        {
            var childProgress = await _childProgressRepository.GetByIdAsync(id);
            if (childProgress == null)
            {
                throw new Exception($"ChildProgress with ID {id} not found");
            }

            if (childProgress.IsDeleted)
            {
                childProgress.IsDeleted = false;
                await _childProgressRepository.UpdateAsync(childProgress);
            }

            return childProgress;
        }

        public async Task<List<ChildProgress>> SearchChildProgresses(SearchChildProgressDTO searchChildProgressDTO)
        {
            return await _childProgressRepository.SearchChildProgresses(searchChildProgressDTO);
        }
    }
}
