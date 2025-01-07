using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IEventService
    {
        Task<IEnumerable<EventResponseDTO>> GetAllEvent();
        Task<EventResponseDTO> GetEventById(int id);
        Task<Event> CreateEvent(CreateEventDTO createEvent);
        Task<Event> UpdateEvent(int id, UpdateEventDTO updateEvent);
        Task<Event> DeleteEvent(int id);
        Task<Event> RestoreEvent(int id);
        Task<string> DonateEvent(int id, EventDonateDTO updateEvent);
        public Task<EventResponseDTO[]> GetAllEventIsDeleteAsync();
        Task<EventDetailsDTO> GetEventDetails(int eventId);
        Task<List<Event>> SearchEvents(SearchEventDTO searchEventDTO);
    }
}
