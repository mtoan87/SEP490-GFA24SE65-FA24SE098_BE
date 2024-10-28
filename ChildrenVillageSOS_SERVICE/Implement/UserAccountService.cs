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
        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
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
            // Lấy ID hiện tại cao nhất từ bảng UserAccount
            var highestIdUser = await _userAccountRepository.GetHighestIdUser();
            int currentHighestIdNumber = 0;

            if (highestIdUser != null)
            {
                // Tách số từ ID hiện tại cao nhất (VD: UA013 -> 13)
                var highestId = highestIdUser.Id;
                currentHighestIdNumber = int.Parse(highestId.Substring(2));
            }

            // Tăng số tự động lên 1
            var newIdNumber = currentHighestIdNumber + 1;
            var newId = $"UA{newIdNumber:D3}"; // D3 định dạng số với ít nhất 3 chữ số, thêm 0 ở trước nếu cần

            var newUser = new UserAccount
            {
                Id = newId,
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
            return newUser;
        }

        public async Task<UserAccount> UpdateUser(string id, UpdateUserDTO updateUser)
        {
            var updaUser = await _userAccountRepository.GetByIdAsync(id);
            if (updaUser == null)
            {
                throw new Exception($"Expense with ID{id} not found!");
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
