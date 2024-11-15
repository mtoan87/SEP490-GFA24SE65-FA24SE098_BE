using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IChildRepository : IRepositoryGeneric<Child>
    {
        Task<IEnumerable<Child>> GetAllAsync();
        ChildResponseDTO GetChildByIdWithImg(string childId);
    }
}
