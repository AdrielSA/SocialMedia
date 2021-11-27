using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserService
    {
        Task InsertUser(User oUser);

        IEnumerable<User> GetUsers(UserQueryFilter filter);

        Task<User> GetUser(int id);

        Task<bool> UpdateUser(User oUser);

        Task<bool> DeleteUser(int id);
    }
}
