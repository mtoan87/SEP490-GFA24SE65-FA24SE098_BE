using ChildrenVillageSOS_DAL.DTO.AuthDTO;
using ChildrenVillageSOS_DAL.DTO.ChildDTO;
using ChildrenVillageSOS_DAL.DTO.UserDTO;
using ChildrenVillageSOS_DAL.Helpers;
using ChildrenVillageSOS_DAL.Models;
using ChildrenVillageSOS_REPO.Implement;
using ChildrenVillageSOS_REPO.Interface;
using ChildrenVillageSOS_SERVICE.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Google.Apis.Oauth2.v2.Data;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable getUser()
        {
            return _userAccountRepository.getUser();
        }
        public async Task<IEnumerable<UserAccount>> GetAllUser()
        {
            return await _userAccountRepository.GetAllNotDeletedAsync();
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
        public async Task<UserResponseDTO> GetUserByIdArray(string userid)
        {
            return _userAccountRepository.GetUserByIdArray(userid);
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

            var existingImages = await _imageRepository.GetByUserAccountIdAsync(updaUser.Id);

            // Xóa các ảnh được yêu cầu xóa
            if (updateUser.ImgToDelete != null && updateUser.ImgToDelete.Any())
            {
                foreach (var imageIdToDelete in updateUser.ImgToDelete)
                {
                    var imageToDelete = existingImages.FirstOrDefault(img => img.UrlPath == imageIdToDelete);
                    if (imageToDelete != null)
                    {
                        imageToDelete.IsDeleted = true;
                        imageToDelete.ModifiedDate = DateTime.Now;

                        // Cập nhật trạng thái ảnh trong database
                        await _imageRepository.UpdateAsync(imageToDelete);

                        // Xóa ảnh khỏi Cloudinary
                        bool isDeleted = await _imageService.DeleteImageAsync(imageToDelete.UrlPath, "UserAccountImages");
                        if (isDeleted)
                        {
                            await _imageRepository.RemoveAsync(imageToDelete);
                        }
                    }
                }
            }

            // Thêm các ảnh mới nếu có
            if (updateUser.Img != null && updateUser.Img.Any())
            {
                var newImageUrls = await _imageService.UploadUserAccountImage(updateUser.Img, updaUser.Id);
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
            else
            {
            }

            // Lưu thông tin cập nhật
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
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }

            if (user.IsDeleted == true)
            {
                // Hard delete nếu đã bị soft delete
                await _userAccountRepository.RemoveAsync(user);
            }
            else
            {
                // Soft delete: đặt IsDeleted = true
                user.IsDeleted = true;
                await _userAccountRepository.UpdateAsync(user);
            }
            return user;
        }

        public async Task<UserAccount> RestoreUser(string id)
        {
            var user = await _userAccountRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }

            if (user.IsDeleted == true) // Nếu đã bị soft delete
            {
                user.IsDeleted = false; // Khôi phục bằng cách đặt IsDeleted = false
                await _userAccountRepository.UpdateAsync(user);
            }
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

        public Task<UserResponseDTO[]> GetAllUserIsDeletedAsync()
        {
            return _userAccountRepository.GetAllUserIsDeletedAsync();
        }

        public async Task<GetAuthTokenDTO> LoginWithGoogle(string googleToken)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://oauth2.googleapis.com/tokeninfo?id_token=" + googleToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var userInfoJson = JObject.Parse(json);
                var userInfo = new Userinfo
                {
                    Id = userInfoJson["sub"]?.ToString(),
                    Email = userInfoJson["email"]?.ToString(),
                };

                var existAccount = await _userAccountRepository.GetAsync(x => x.UserEmail == userInfo.Email);
                if (existAccount is not null)
                {
                    return new GetAuthTokenDTO()
                    {
                        AccessToken = googleToken,
                        UserAccountId = existAccount.Id,
                        RoleId = existAccount.RoleId
                    };
                }

                var allUserId = await _userAccountRepository.Entities().Select(u => u.Id).ToListAsync();
                string newUserId = IdGenerator.GenerateId(allUserId, "UA");
                var newAccount = new UserAccount
                {
                    Id = newUserId,
                    UserEmail = userInfo.Email!,
                    RoleId = 2,
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                };
                await _userAccountRepository.AddAsync(newAccount);
                await _userAccountRepository.SaveChangesAsync();

                return new GetAuthTokenDTO()
                {
                    AccessToken = googleToken,
                    UserAccountId = newAccount.Id,
                    RoleId = newAccount.RoleId
                };
            }

            return new GetAuthTokenDTO()
            {
                AccessToken = null
            };
        }

        public async Task<List<UserAccount>> SearchUserAccounts(SearchUserDTO searchUserDTO)
        {
            return await _userAccountRepository.SearchUserAccounts(searchUserDTO);
        }
    }
}
