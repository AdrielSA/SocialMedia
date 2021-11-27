using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filters);

        Task<Post> GetPost(int id);

        Task InsertPost(Post oPost);

        Task<bool> UpdatePost(Post oPost);

        Task<bool> DeletePost(int id);
    }
}