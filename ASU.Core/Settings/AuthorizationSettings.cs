namespace ASU.Core.Settings
{
    public class AuthorizationSettings
    {
        public string Authority { get; set; }
        public string Issuer { get; set; }
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
