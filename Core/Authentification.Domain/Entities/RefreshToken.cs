namespace Authentification.Domain.Entities
{
    public class RefreshToken
    {
        public Guid TokenId { get; set; }
        public Guid UserId { get; set; }


        public string Token { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public User User { get; set; }
    }
}
