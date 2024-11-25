using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IUserAccountService
    {
        DataTable getUser();
        Task<IEnumerable<UserAccount>> GetAllUser();
        Task<UserAccount> GetUserById(string id);
        Task<UserAccount> CreateUser(CreateUserDTO createUser);
        Task<UserAccount> UpdateUser(string id, UpdateUserDTO updateUser);
        Task ChangePassword(string id, ChangePassUserDTO changePassUserDTO);
        Task<UserAccount> DeleteUser(string id);
        Task<UserAccount> RestoreUser(string id);
        Task<UserAccount> Login(string email, string password);
    }
}
