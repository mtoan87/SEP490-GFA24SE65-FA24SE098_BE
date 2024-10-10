using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class ChildService : IChildService
    {
        private readonly IChildRepository _childRepository;

        public ChildService(IChildRepository childRepository)
        {
            _childRepository = childRepository;
        }

        //public async Task<IEnumerable<Child>> GetAllChildren()
        //{
        //    return await _childRepository.GetAllAsync();
        //}

        public async Task<IEnumerable<Child>> GetAllChildren()
        {
            return await _childRepository.GetAllNotDeletedAsync();
        }

        public async Task<Child> GetChildById(string id)
        {
            return await _childRepository.GetByIdAsync(id);
        }

        public async Task<Child> CreateChild(CreateChildDTO createChild)
        {
            // Lấy toàn bộ danh sách ChildId hiện có
            var allChildIds = await _childRepository.Entities()
                                                    .Select(c => c.Id)
                                                    .ToListAsync();

            // Sử dụng hàm GenerateId từ IdGenerator
            string newChildId = IdGenerator.GenerateId(allChildIds, "C");

            var newChild = new Child
            {
                Id = newChildId,  // Gán ID mới
                ChildName = createChild.ChildName,
                HealthStatus = createChild.HealthStatus,
                HouseId = createChild.HouseId,
                Gender = createChild.Gender,
                Dob = createChild.Dob,
                Status = createChild.Status,
                IsDeleted = createChild.IsDeleted
            };
            await _childRepository.AddAsync(newChild);
            return newChild;
        }

        public async Task<Child> UpdateChild(string id, UpdateChildDTO updateChild)
        {
            var existingChild = await _childRepository.GetByIdAsync(id);
            if (existingChild == null)
            {
                throw new Exception($"Child with ID {id} not found!");
            }

            existingChild.ChildName = updateChild.ChildName;
            existingChild.HealthStatus = updateChild.HealthStatus;
            existingChild.HouseId = updateChild.HouseId;
            existingChild.Gender = updateChild.Gender;
            existingChild.Dob = updateChild.Dob;
            existingChild.Status = updateChild.Status;
            existingChild.IsDeleted = updateChild.IsDeleted;

            await _childRepository.UpdateAsync(existingChild);
            return existingChild;
        }

        public async Task<Child> DeleteChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true)
            {
                // Hard delete
                await _childRepository.RemoveAsync(child);
            }
            else
            {
                // Soft delete (đặt IsDeleted = true)
                child.IsDeleted = true;
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }

        public async Task<Child> RestoreChild(string id)
        {
            var child = await _childRepository.GetByIdAsync(id);
            if (child == null)
            {
                throw new Exception($"Child with ID {id} not found");
            }

            if (child.IsDeleted == true) // Nếu đã bị soft delete
            {
                child.IsDeleted = false;  // Khôi phục bằng cách đặt lại IsDeleted = false
                await _childRepository.UpdateAsync(child);
            }
            return child;
        }
    }
}
