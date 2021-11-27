using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IAuthService
    {
        Task<(bool, Security)> IsValidUser(UserLogin login);

        string GenerateToken(Security security);
    }
}
