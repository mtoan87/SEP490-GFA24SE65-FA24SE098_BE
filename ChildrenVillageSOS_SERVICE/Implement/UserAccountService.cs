using ChildrenVillageSOS_DAL.DTO.UserDTO;
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
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IImageService _imageService;
        private readonly IImageRepository _imageRepository;
        public UserAccountService(IUserAccountRepository userAccountRepository, IImageService imageService, IImageRepository imageRepository)
        {
            _userAccountRepository = userAccountRepository;
            _imageService = imageService;
            _imageRepository = imageRepository;
        }
        public async Task<IEnumerable<UserAccount>> GetAllUser()
        {
            return await _userAccountRepository.GetAllAsync();
        }
        public async Task<UserAccount> GetUserById(string id)
        {
            return await _userAccountRepository.GetByIdAsync(id);
        }
        public async Task<UserAccount> CreateUser(CreateUserDTO createUser)
        {
            var newUser = new UserAccount
            {
                Id = createUser.Id,
                UserName = createUser.UserName,
                UserEmail = createUser.UserEmail,
                Password = createUser.Password,
                Phone = createUser.Phone,
                Address = createUser.Address,
/*                Dob = createUser.Dob,*/
                Gender = createUser.Gender,
                Country = createUser.Country,
                RoleId = createUser.RoleId,
                CreatedDate = createUser.CreatedDate,
            };
            await _userAccountRepository.AddAsync(newUser);

            string url = await _imageService.UploadUserAccountImage(createUser.Img, newUser.Id);
            var image = new Image
            {
                UrlPath = url,
                UserAccountId = newUser.Id,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            await _imageRepository.AddAsync(image);
            return newUser;
        }
        public async Task<UserAccount> UpdateUser(string id, UpdateUserDTO updateUser)
        {
            var updaUser = await _userAccountRepository.GetByIdAsync(id);
            if (updaUser == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
            }
            updateUser.UserName = updaUser.UserName;
            updateUser.UserEmail = updaUser.UserEmail;
            updateUser.Password = updaUser.Password;
            updateUser.Phone = updaUser.Phone;
            updateUser.Address = updaUser.Address;
/*            updateUser.Dob = updaUser.Dob;*/
            updateUser.Gender = updaUser.Gender;
            updateUser.Country = updaUser.Country;
            updateUser.RoleId = updaUser.RoleId;
            updateUser.ModifiedDate = updaUser.ModifiedDate;

            if (updateUser.Img != null)
            {
                var existingImage = await _imageRepository.GetByUserAccountIdAsync(updaUser.Id);

                if (existingImage != null)
                {
                    // Xóa ảnh cũ trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "UserImages");

                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }

                    // Tải ảnh mới lên Cloudinary và lấy URL
                    string newImageUrl = await _imageService.UploadUserAccountImage(updateUser.Img, updaUser.Id);

                    // Cập nhật URL của ảnh cũ
                    existingImage.UrlPath = newImageUrl;
                    existingImage.ModifiedDate = DateTime.UtcNow;

                    // Lưu thay đổi vào database
                    await _imageRepository.UpdateAsync(existingImage);
                }
                else
                {
                    // Nếu không có ảnh cũ, tạo ảnh mới
                    string newImageUrl = await _imageService.UploadUserAccountImage(updateUser.Img, updaUser.Id);

                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        UserAccountId = updaUser.Id,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };

                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _userAccountRepository.UpdateAsync(updaUser);
            return updaUser;
        }
        public async Task<UserAccount> DeleteUser(string id)
        {
            var user = await _userAccountRepository.GetByIdAsync(id);
            if(user == null)
            {
                throw new Exception($"User with ID{id} not found");
            }
            await _userAccountRepository.RemoveAsync(user);
            return user;    
        }
        public async Task<UserAccount> Login(string email, string password)
        {
            var login = await _userAccountRepository.Login(email, password);
            if (login == null)
            {
                throw new Exception("Login Fail!");
            }
            return login;
        }
    }
}
