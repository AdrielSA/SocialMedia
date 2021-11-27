namespace SocialMedia.Core.QueryFilters
{
    public class UserQueryFilter
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public bool? IsActive { get; set; }
    }
}
