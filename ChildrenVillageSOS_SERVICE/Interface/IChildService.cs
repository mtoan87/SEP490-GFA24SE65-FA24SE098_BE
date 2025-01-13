using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IChildService
    {
        Task<IEnumerable<Child>> GetAllChildren();
        Task<IEnumerable<ChildResponseDTO>> GetAllChildrenWithImg();
        Task<IEnumerable<ChildResponseDTO>> GetAllChildrenWithHealthStatusBad();
        Task<Child> GetChildById(string id);
        Task<ChildResponseDTO> GetChildByIdWithImg(string childid);
        Task<Child> CreateChild(CreateChildDTO createChild);
        Task<ChildResponseDTO[]> SearchChildrenAsync(string searchTerm);
        Task<Child> UpdateChild(string id, UpdateChildDTO updateChild);
        Task<Child> DeleteChild(string id);
        Task<Child> RestoreChild(string id);
        Task<string> DonateChild(string id, ChildDonateDTO updateChild);
        Task<List<Child>> GetChildByHouseIdAsync(string houseId);
        Task<ChildResponseDTO[]> GetAllChildIsDeleteAsync();
        Task<ChildDetailsDTO> GetChildDetails(string childId);
        Task<ChildResponseDTO[]> GetChildByHouseId(string houseId);
        Task<List<Child>> SearchChildren(SearchChildDTO searchChildDTO);
    }
}
