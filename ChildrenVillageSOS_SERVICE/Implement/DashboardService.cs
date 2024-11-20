using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_REPO.Implement;
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
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IEventRepository _eventRepository;

        public DashboardService(IChildRepository childRepository, IUserAccountRepository userAccountRepository, IEventRepository eventRepository)
        {
            _childRepository = childRepository;
            _userAccountRepository = userAccountRepository;
            _eventRepository = eventRepository;
        }

        //TopStatCards
        public async Task<ActiveChildrenStatDTO> GetActiveChildrenStatAsync()
        {
            return await _childRepository.GetActiveChildrenStatAsync();
        }

        public async Task<TotalEventsStatDTO> GetTotalEventsStatAsync()
        {
            return await _eventRepository.GetTotalEventsStatAsync();
        }

        public async Task<TotalUsersStatDTO> GetTotalUsersStatAsync()
        {
            return await _userAccountRepository.GetTotalUsersStatAsync();
        }
    }
}
