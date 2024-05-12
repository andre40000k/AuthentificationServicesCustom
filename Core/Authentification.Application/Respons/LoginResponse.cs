namespace Authentification.Application.Respons
{
    public class LoginResponse
    {
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public TokenResponse TokenResponse { get; set; }
    }
}
