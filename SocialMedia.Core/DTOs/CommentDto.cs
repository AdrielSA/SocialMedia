using System;

namespace SocialMedia.Core.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
        
        public bool IsActive { get; set; }
    }
}
