using ChildrenVillageSOS_DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrenVillageSOS_SERVICE.Interface
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetAllImages();
        Task<bool> DeleteImageAsync(string urlImage, string path);
        Task<string> UploadChildImage(IFormFile file, string ChildId);
        Task<string> UploadHouseImage(IFormFile file, string HouseId);
        Task<string> UploadVillageImage(IFormFile file, string VillageId);
        Task<string> UploadEventImage(IFormFile file, int EventId);
        Task<string> UploadUserAccountImage(IFormFile file, string UserAccountId);

    }
}
