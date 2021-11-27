using System;

namespace SocialMedia.Core.QueryFilters
{
    public class CommentQueryFilter
    {
        public int PostId { get; set; }

        public string Description { get; set; }
        
        public DateTime? Date { get; set; }
        
        public bool? IsActive { get; set; }
    }
}
