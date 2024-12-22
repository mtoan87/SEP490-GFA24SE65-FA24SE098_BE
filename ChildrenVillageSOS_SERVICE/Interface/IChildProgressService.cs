using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IChildProgressService
    {
        Task<IEnumerable<ChildProgress>> GetAllChildProgresses();
        Task<ChildProgress> GetChildProgressById(int id);
        Task<ChildProgress> CreateChildProgress(CreateChildProgressDTO createChildProgress);
        Task<ChildProgress> UpdateChildProgress(int id, UpdateChildProgressDTO updateChildProgress);
        Task<ChildProgress> DeleteChildProgress(int id);
        Task<ChildProgress> RestoreChildProgress(int id);
    }
}
