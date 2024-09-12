using ChildrenVillageSOS_DAL.DTO.RoleDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRole();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(CreateRoleDTO createRole);
        Task<Role> UpdateRole(int id, UpdateRoleDTO updateRole);
        Task<Role> DeleteRole(int id);
    }
}
