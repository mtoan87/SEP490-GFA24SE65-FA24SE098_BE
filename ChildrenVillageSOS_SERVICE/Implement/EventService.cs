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

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadEventImage(createEvent.Img, newEvent.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    EventId = newEvent.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
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

            // Nếu có danh sách ảnh được upload trong yêu cầu cập nhật
            if (updateEvent.Img != null && updateEvent.Img.Any())
            {
                // Lấy danh sách ảnh hiện tại của KoiFishy từ database
                var existingImages = await _imageRepository.GetByEventIdAsync(editEvent.Id);

                // Xóa tất cả các ảnh cũ trên Cloudinary và trong cơ sở dữ liệu
                foreach (var existingImage in existingImages)
                {
                    // Xóa ảnh trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "EventImages");
                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }
                    // Xóa ảnh khỏi database
                    await _imageRepository.RemoveAsync(existingImage);
                }

                // Upload danh sách ảnh mới và lưu thông tin vào database
                List<string> newImageUrls = await _imageService.UploadEventImage(updateEvent.Img, editEvent.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        EventId = editEvent.Id,
                        ModifiedDate = DateTime.Now,
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
