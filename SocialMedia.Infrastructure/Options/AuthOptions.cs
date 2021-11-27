namespace SocialMedia.Infrastructure.Options
{
    public class AuthOptions
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ValidTime { get; set; }
    }
}
