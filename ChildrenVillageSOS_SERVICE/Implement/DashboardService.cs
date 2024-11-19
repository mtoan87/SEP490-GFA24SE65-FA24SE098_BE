using ChildrenVillageSOS_DAL.DTO.DashboardDTO;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class DashboardService : IDashboardService
    {
        private readonly IChildRepository _childRepository;

        public DashboardService(IChildRepository childRepository)
        {
            _childRepository = childRepository;
        }

        //TopStatCards
        public async Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync()
        {
            return await _childRepository.GetActiveChildrenStatAsync();
        }
    }
}
