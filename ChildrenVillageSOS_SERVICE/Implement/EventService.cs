using ChildrenVillageSOS_DAL.DTO.EventDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
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
                Name = createEvent.Name,
                Description = createEvent.Description,
                StartTime = createEvent.StartTime,
                EndTime = createEvent.EndTime,
                Status = "Active",
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Amount = createEvent.Amount,
                AmountLimit = createEvent.AmountLimit,
                ChildId = createEvent.ChildId,
            };
            await _eventRepository.AddAsync(newEvent);

            string url = await _imageService.UploadEventImage(createEvent.Img, newEvent.Id);
            var image = new Image
            {
                UrlPath = url,
                EventId = newEvent.Id,
                CreatedDate = DateTime.Now,
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

            editEvent.Name = updateEvent.Name;
            editEvent.Description = updateEvent.Description;
            editEvent.StartTime = updateEvent.StartTime;
            editEvent.EndTime = updateEvent.EndTime;
            editEvent.Status = updateEvent.Status;
            editEvent.ModifiedDate = DateTime.Now;
            editEvent.IsDeleted = updateEvent.IsDeleted;
            editEvent.Amount = updateEvent.Amount;
            editEvent.AmountLimit = updateEvent.AmountLimit;
            editEvent.ChildId = updateEvent.ChildId;

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
                    existingImage.ModifiedDate = DateTime.Now;

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
                        CreatedDate = DateTime.Now,
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
