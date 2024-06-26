﻿namespace Authentification.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}
