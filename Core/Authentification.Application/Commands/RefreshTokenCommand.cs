namespace Authentification.Application.Commands
{
    public class RefreshTokenCommand
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
