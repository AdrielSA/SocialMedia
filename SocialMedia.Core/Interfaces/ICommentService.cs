using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPost(CommentQueryFilter filter);

        Task<Comment> GetComment(int id);

        Task InsertComment(Comment oComment);

        Task<bool> UpdateComment(Comment oComment);

        Task<bool> DeleteComment(int id);
    }
}
