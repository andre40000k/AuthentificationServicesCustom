namespace Authentification.Application.Commands
{
    public class RevokeTokenCommand
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
