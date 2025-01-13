using ChildrenVillageSOS_DAL.DTO.DashboardDTO.TopStatCards;
using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_REPO.Interface
{
    public interface IEventRepository : IRepositoryGeneric<Event>
    {
        Task<IEnumerable<Event>> GetAllAsync();
        EventResponseDTO GetEventById(int eventId);
        Task<TotalEventsStatDTO> GetTotalEventsStatAsync();
        Task<EventResponseDTO[]> GetAllEventIsDeleteAsync();
        Task<EventDetailsDTO> GetEventDetails(int eventId);
        Task<List<Event>> SearchEvents(SearchEventDTO searchEventDTO);
        Task<EventResponseDTO[]> GetAllEventArrayAsync();
        Task<EventResponseDTO[]> SearchEventArrayAsync(string searchTerm);
    }
}
