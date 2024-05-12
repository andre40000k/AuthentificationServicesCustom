namespace Authentification.Application.Request
{
    public class TokenApiRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
