using ChildrenVillageSOS_DAL.DTO.ChildProgressDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IChildProgressRepository : IRepositoryGeneric<ChildProgress>
    {
        Task<List<ChildProgress>> SearchChildProgresses(SearchChildProgressDTO searchChildProgressDTO);
    }
}
