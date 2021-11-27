using SocialMedia.Core.Entities;
using SocialMedia.Core.Exeptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task InsertUser(User oUser)
        {
            await _unitOfWork.UserRepository.Add(oUser);
            await _unitOfWork.SavesChangesAsync();
        }

        public IEnumerable<User> GetUsers(UserQueryFilter filter)
        {
            var users = _unitOfWork.UserRepository.GetAll();
            if (filter.FirstName != null)
            {
                users = users.Where(u => u.FirstName.ToLower() == filter.FirstName.ToLower());
            }
            if (filter.LastName != null)
            {
                users = users.Where(u => u.LastName.ToLower() == filter.LastName.ToLower());
            }
            if (filter.Email != null)
            {
                users = users.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            users = filter.IsActive == true ? users.Where(u => u.IsActive == true) : 
                    filter.IsActive == false ? users.Where(u => u.IsActive == false) : users;

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<bool> UpdateUser(User oUser)
        {
            var eUser = await _unitOfWork.UserRepository.GetById(oUser.Id);
            eUser.FirstName = oUser.FirstName;
            eUser.LastName = oUser.LastName;
            eUser.Email = oUser.Email;
            eUser.Telephone = oUser.Telephone;
            eUser.DateOfBirth = oUser.DateOfBirth;
            eUser.IsActive = oUser.IsActive;
            _unitOfWork.UserRepository.Update(eUser);
            await _unitOfWork.SavesChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SavesChangesAsync();
            return true;
        }
    }
}
