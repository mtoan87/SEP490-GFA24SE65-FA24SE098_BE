using ChildrenVillageSOS_DAL.DTO.AuthDTO;
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
        Task<UserAccount> UserUpdate(string id, UserUpdate updateUser);
        string? GetRoleNameById(int roleId);
        DataTable getUser();
        Task<IEnumerable<UserAccount>> GetAllUser();
        Task<UserAccount> GetUserById(string id);
        Task<UserAccount> CreateUser(CreateUserDTO createUser);
        Task<UserAccount> UpdateUser(string id, UpdateUserDTO updateUser);
        Task ChangePassword(string id, ChangePassUserDTO changePassUserDTO);
        Task<UserAccount> DeleteUser(string id);
        Task<UserAccount> RestoreUser(string id);
        Task<UserAccount> Login(string email, string password);
        public string GenerateToken(UserAccount user);
        Task<UserResponseDTO[]> GetAllUserIsDeletedAsync();
        Task<UserResponseDTO[]> GetAllUserArrayAsync();
        Task<UserResponseDTO> GetUserByIdArray(string userid);
        Task<GetAuthTokenDTO> LoginWithGoogle(string googleToken);
        Task<List<UserAccount>> SearchUserAccounts(SearchUserDTO searchUserDTO);
        Task<UserResponseDTO[]> SearchUserArrayAsync(string searchUserDTO);
    }
}
