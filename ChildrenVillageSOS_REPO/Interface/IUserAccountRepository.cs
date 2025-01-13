using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IUserAccountRepository : IRepositoryGeneric<UserAccount>
    {
        string? GetRoleNameById(int roleId);
        Task<UserAccount> Login(string email, string password);
        Task<UserAccount> GetHighestIdUser();

        Task<UserAccount> GetUserWithImagesByIdAsync(string id);
        Task<TotalUsersStatDTO> GetTotalUsersStatAsync(); //Dashboard
        DataTable getUser();
        Task<UserResponseDTO[]> GetAllUserIsDeletedAsync();
        Task<UserResponseDTO[]> GetAllUserArrayAsync();
        UserResponseDTO GetUserByIdArray(string userid);
        Task<List<UserAccount>> SearchUserAccounts(SearchUserDTO searchUserDTO);
        Task<UserResponseDTO[]> SearchUserArrayAsync(string searchTerm);
    }
}
