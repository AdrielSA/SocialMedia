using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SocialMediaContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetCommentsByPost(int postId)
        {
            return await _entities.Where(c => c.PostId == postId).ToListAsync();
        }
    }
}
