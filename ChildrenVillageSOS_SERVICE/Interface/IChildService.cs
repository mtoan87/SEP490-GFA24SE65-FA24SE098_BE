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
        Task<Child> GetChildById(string id);
        Task<Child> CreateChild(CreateChildDTO createChild);
        Task<Child> UpdateChild(string id, UpdateChildDTO updateChild);
        Task<Child> DeleteChild(string id);
        Task<Child> RestoreChild(string id);
    }
}
