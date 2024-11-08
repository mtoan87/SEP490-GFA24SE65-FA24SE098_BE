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
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public EventService(IEventRepository eventRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _eventRepository = eventRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
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

            string url = await _imageService.UploadEventImage(createEvent.Img, newEvent.Id);
            var image = new Image
            {
                UrlPath = url,
                EventId = newEvent.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
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

            if (updateEvent.Img != null)
            {
                var existingImage = await _imageRepository.GetByEventIdAsync(editEvent.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "EventImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadEventImage(updateEvent.Img, editEvent.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.UtcNow;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadEventImage(updateEvent.Img, editEvent.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        EventId = editEvent.Id,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

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
