using ChildrenVillageSOS_DAL.DTO.ChildNeedsDTO;
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
    public class ChildNeedService : IChildNeedService
    {
        private readonly IChildNeedRepository _childNeedRepository;
        private readonly IImageService _imageService;

        public ChildNeedService(IChildNeedRepository childNeedRepository, IImageService imageService)
        {
            _childNeedRepository = childNeedRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<ChildNeed>> GetAllChildNeeds()
        {
            return await _childNeedRepository.GetAllNotDeletedAsync();
        }

        public async Task<ChildNeed> GetChildNeedById(int id)
        {
            return await _childNeedRepository.GetByIdAsync(id);
        }

        public async Task<ChildNeed> CreateChildNeed(CreateChildNeedsDTO createChildNeed)
        {
            var newChildNeed = new ChildNeed
            {
                ChildId = createChildNeed.ChildId,
                NeedDescription = createChildNeed.NeedDescription,
                NeedType = createChildNeed.NeedType,
                Priority = createChildNeed.Priority,
                FulfilledDate = createChildNeed.FulfilledDate,
                Remarks = createChildNeed.Remarks,
                Status = createChildNeed.Status,
                CreatedBy = createChildNeed.CreatedBy,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _childNeedRepository.AddAsync(newChildNeed);
            return newChildNeed;
        }

        public async Task<ChildNeed> UpdateChildNeed(int id, UpdateChildNeedsDTO updateChildNeed)
        {
            var existingChildNeed = await _childNeedRepository.GetByIdAsync(id);
            if (existingChildNeed == null)
            {
                throw new Exception($"ChildNeed with ID {id} not found!");
            }

            existingChildNeed.NeedDescription = updateChildNeed.NeedDescription;
            existingChildNeed.NeedType = updateChildNeed.NeedType;
            existingChildNeed.Priority = updateChildNeed.Priority;
            existingChildNeed.FulfilledDate = updateChildNeed.FulfilledDate;
            existingChildNeed.Remarks = updateChildNeed.Remarks;
            existingChildNeed.Status = updateChildNeed.Status;
            existingChildNeed.ModifiedBy = updateChildNeed.ModifiedBy;
            existingChildNeed.ModifiedDate = DateTime.Now;

            await _childNeedRepository.UpdateAsync(existingChildNeed);
            return existingChildNeed;
        }

        public async Task<ChildNeed> DeleteChildNeed(int id)
        {
            var childNeed = await _childNeedRepository.GetByIdAsync(id);
            if (childNeed == null)
            {
                throw new Exception($"ChildNeed with ID {id} not found");
            }

            if (childNeed.IsDeleted)
            {
                await _childNeedRepository.RemoveAsync(childNeed);
            }
            else
            {
                childNeed.IsDeleted = true;
                await _childNeedRepository.UpdateAsync(childNeed);
            }

            return childNeed;
        }

        public async Task<ChildNeed> RestoreChildNeed(int id)
        {
            var childNeed = await _childNeedRepository.GetByIdAsync(id);
            if (childNeed == null)
            {
                throw new Exception($"ChildNeed with ID {id} not found");
            }

            if (childNeed.IsDeleted)
            {
                childNeed.IsDeleted = false;
                await _childNeedRepository.UpdateAsync(childNeed);
            }

            return childNeed;
        }

        public async Task<List<ChildNeed>> SearchChildNeeds(SearchChildNeedsDTO searchChildNeedsDTO)
        {
            return await _childNeedRepository.SearchChildNeeds(searchChildNeedsDTO);
        }
    }
}
