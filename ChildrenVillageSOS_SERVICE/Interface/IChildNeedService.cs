using ChildrenVillageSOS_DAL.DTO.ChildNeedsDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IChildNeedService
    {
        Task<IEnumerable<ChildNeed>> GetAllChildNeeds();
        Task<ChildNeed> GetChildNeedById(int id);
        Task<ChildNeed> CreateChildNeed(CreateChildNeedsDTO createChildNeed);
        Task<ChildNeed> UpdateChildNeed(int id, UpdateChildNeedsDTO updateChildNeed);
        Task<ChildNeed> DeleteChildNeed(int id);
        Task<ChildNeed> RestoreChildNeed(int id);
        Task<List<ChildNeed>> SearchChildNeeds(SearchChildNeedsDTO searchChildNeedsDTO);
    }
}
