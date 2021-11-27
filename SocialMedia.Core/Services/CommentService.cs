using SocialMedia.Core.Entities;
using SocialMedia.Core.Exeptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPost(CommentQueryFilter filter)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsByPost(filter.PostId);
            if (filter.Description != null)
            {
                comments = comments.Where(c => c.Description.ToLower().Contains(filter.Description.ToLower()));
            }
            if (filter.Date != null)
            {
                comments = comments.Where(c => c.Date.ToShortDateString() == filter.Date?.ToShortDateString());
            }
            comments = filter.IsActive == true ? comments.Where(u => u.IsActive == true) :
                    filter.IsActive == false ? comments.Where(u => u.IsActive == false) : comments;
            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            return await _unitOfWork.CommentRepository.GetById(id);
        }

        public async Task InsertComment(Comment oComment)
        {
            var post = await _unitOfWork.PostRepository.GetById(oComment.PostId);
            if (post == null)
            {
                throw new BusinessException("Post doesn't exist");
            }
            await _unitOfWork.CommentRepository.Add(oComment);
            await _unitOfWork.SavesChangesAsync();
        }

        public async Task<bool> UpdateComment(Comment oComment)
        {
            var eComment = await _unitOfWork.CommentRepository.GetById(oComment.Id);
            eComment.Description = oComment.Description;
            eComment.Date = oComment.Date;
            eComment.IsActive = oComment.IsActive;
            await _unitOfWork.SavesChangesAsync();
            return true;
        }

        public async Task<bool> DeleteComment(int id)
        {
            await _unitOfWork.CommentRepository.Delete(id);
            await _unitOfWork.SavesChangesAsync();
            return true;
        }
    }
}
