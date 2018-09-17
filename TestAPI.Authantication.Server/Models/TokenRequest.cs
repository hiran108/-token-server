namespace TestAPI.Authantication.Server.Models
{
    public class TokenRequest
    {
        public string username { get; set; }
        public string password { get; set; }

        public string audience { get; set; }
    }
}