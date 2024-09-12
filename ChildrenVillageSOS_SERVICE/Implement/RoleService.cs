using ChildrenVillageSOS_DAL.DTO.RoleDTO;
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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _roleRepository.GetAllAsync();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }
        public async Task<Role> CreateRole(CreateRoleDTO createRole)
        {
            var newRole = new Role
            {
                RoleName = createRole.RoleName,
            };
            await _roleRepository.AddAsync(newRole);
            return newRole;
        }
        public async Task<Role> UpdateRole(int id, UpdateRoleDTO updateRole)
        {
            var updaRole = await _roleRepository.GetByIdAsync(id);
            if(updaRole == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updaRole.RoleName = updateRole.RoleName;
            await _roleRepository.UpdateAsync(updaRole);
            return updaRole;
        }
        public async Task<Role> DeleteRole(int id)
        {
            var deleRole = await _roleRepository.GetByIdAsync(id);
            if(deleRole == null)
            {
                throw new Exception($"Role with ID{id} not found");
            }
            await _roleRepository.RemoveAsync(deleRole);
            return deleRole;
        }
    }
}
