using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Implement
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<IEnumerable<Event>> GetAllEvent()
        {
            return await _eventRepository.GetAllAsync();
        }
        public async Task<Event> GetEventById(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }
        public async Task<Event> CreateEvent(CreateEventDTO createEvent)
        {
            var newEvent = new Event
            {
                Id = createEvent.Id,
                Name = createEvent.Name,
                Description = createEvent.Description,
                StartTime = createEvent.StartTime,
                EndTime = createEvent.EndTime,
                Status = createEvent.Status,
                CreatedDate = createEvent.CreatedDate,
                ModifiedDate = createEvent.ModifiedDate,
                Amount = createEvent.Amount,
                ChildId = createEvent.ChildId,
            };
            await _eventRepository.AddAsync(newEvent);
            return newEvent;
        }
        public async Task<Event> UpdateEvent(int id ,UpdateEventDTO updateEvent)
        {
            var editEvent = await _eventRepository.GetByIdAsync(id);
            if(editEvent == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updateEvent.Name = editEvent.Name;
            updateEvent.Description = editEvent.Description;
            updateEvent.StartTime = editEvent.StartTime;
            updateEvent.EndTime = editEvent.EndTime;
            updateEvent.Status = editEvent.Status;
            updateEvent.ModifiedDate = editEvent.ModifiedDate;
            updateEvent.IsDeleted = editEvent.IsDeleted;
            updateEvent.Amount = editEvent.Amount;
            updateEvent.ChildId = editEvent.ChildId;
            await _eventRepository.UpdateAsync(editEvent);
            return editEvent;
        }
        public async Task<Event> DeleteEvent(int id)
        {
            var even = await _eventRepository.GetByIdAsync(id);
            if(even == null)
            {
                throw new Exception($"Event with ID{id} not found");
            }
            await _eventRepository.RemoveAsync(even);
            return even;
        }
    }
}
