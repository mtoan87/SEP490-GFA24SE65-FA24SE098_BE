using ChildrenVillageSOS_DAL.DTO.ChildNeedsDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IChildNeedRepository : IRepositoryGeneric<ChildNeed>
    {
        Task<List<ChildNeed>> SearchChildNeeds(SearchChildNeedsDTO searchChildNeedsDTO);
    }
}
