using ChildrenVillageSOS_DAL.DTO.UserDTO;
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
            return await _userAccountRepository.GetUserWithImagesByIdAsync(id);
        }

        public async Task<UserAccount> CreateUser(CreateUserDTO createUser)
        {
            var allUserId = await _userAccountRepository.Entities().Select(u => u.Id).ToListAsync();
            string newUserId = IdGenerator.GenerateId(allUserId, "UA");
            var newUser = new UserAccount
            {
                Id = newUserId,
                UserName = createUser.UserName,
                UserEmail = createUser.UserEmail,
                Password = createUser.Password,
                Phone = createUser.Phone,
                Address = createUser.Address,
                Dob = createUser.Dob,
                Gender = createUser.Gender,
                Country = createUser.Country,
                RoleId = 2,
                Status = "Active",
                CreatedDate = DateTime.Now,
            };
            await _userAccountRepository.AddAsync(newUser);

            // Upload danh sách ảnh và nhận về các URL
            List<string> imageUrls = await _imageService.UploadUserAccountImage(createUser.Img, newUser.Id);

            // Lưu thông tin các ảnh vào bảng Image
            foreach (var url in imageUrls)
            {
                var image = new Image
                {
                    UrlPath = url,
                    UserAccountId = newUser.Id,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                };
                await _imageRepository.AddAsync(image);
            }
            return newUser;
        }
        public async Task<UserAccount> UpdateUser(string id, UpdateUserDTO updateUser)
        {
            var updaUser = await _userAccountRepository.GetByIdAsync(id);
            if (updaUser == null)
            {
                throw new Exception($"User with ID{id} not found!");
            }
            updaUser.UserName = updateUser.UserName;
            updaUser.UserEmail = updateUser.UserEmail;
            updaUser.Password = updateUser.Password;
            updaUser.Phone = updateUser.Phone;
            updaUser.Address = updateUser.Address;
            updaUser.Dob = updateUser.Dob;
            updaUser.Gender = updateUser.Gender;
            updaUser.Country = updateUser.Country;
            updaUser.RoleId = updateUser.RoleId;
            updaUser.Status = updateUser.Status;
            updaUser.ModifiedDate = DateTime.Now;

            // Nếu có danh sách ảnh được upload trong yêu cầu cập nhật
            if (updateUser.Img != null && updateUser.Img.Any())
            {
                // Lấy danh sách ảnh hiện tại của KoiFishy từ database
                var existingImages = await _imageRepository.GetByUserAccountIdAsync(updaUser.Id);

                // Xóa tất cả các ảnh cũ trên Cloudinary và trong cơ sở dữ liệu
                foreach (var existingImage in existingImages)
                {
                    // Xóa ảnh trên Cloudinary
                    bool isDeleted = await _imageService.DeleteImageAsync(existingImage.UrlPath, "UserAccountImages");
                    if (!isDeleted)
                    {
                        throw new Exception("Không thể xóa ảnh cũ trên Cloudinary");
                    }
                    // Xóa ảnh khỏi database
                    await _imageRepository.RemoveAsync(existingImage);
                }

                // Upload danh sách ảnh mới và lưu thông tin vào database
                List<string> newImageUrls = await _imageService.UploadUserAccountImage(updateUser.Img, updaUser.Id);
                foreach (var newImageUrl in newImageUrls)
                {
                    var newImage = new Image
                    {
                        UrlPath = newImageUrl,
                        UserAccountId = updaUser.Id,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _imageRepository.AddAsync(newImage);
                }
            }

            await _userAccountRepository.UpdateAsync(updaUser);
            return updaUser;
        }
        public async Task ChangePassword(string id, ChangePassUserDTO changePassUserDTO)
        {
            // Tìm kiếm người dùng theo ID
            var user = await _userAccountRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found!");
            }

            // Xác minh mật khẩu hiện tại
            if (user.Password != changePassUserDTO.Password)
            {
                throw new Exception("Current password is incorrect!");
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu
            if (changePassUserDTO.NewPassword != changePassUserDTO.ConfirmPassword)
            {
                throw new Exception("New password and confirm password do not match!");
            }

            // Cập nhật mật khẩu mới
            user.Password = changePassUserDTO.NewPassword;
            user.ModifiedDate = DateTime.Now;

            await _userAccountRepository.UpdateAsync(user);
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
