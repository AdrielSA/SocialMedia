using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }

        IBaseRepository<User> UserRepository { get; }

        ICommentRepository CommentRepository { get; }

        ISecurityRepository SecurityRepository { get; }

        void SavesChanges();

        Task SavesChangesAsync();
    }
}
