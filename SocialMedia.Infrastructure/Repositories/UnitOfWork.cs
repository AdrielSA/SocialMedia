using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISecurityRepository _securityRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public IBaseRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public ICommentRepository CommentRepository => _commentRepository ?? new CommentRepository(_context);

        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SavesChanges()
        {
            _context.SaveChanges();
        }

        public async Task SavesChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
