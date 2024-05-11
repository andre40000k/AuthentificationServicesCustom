namespace Authentification.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string RefreshToken { get; set; }
    }
}
