namespace Authentification.Application.Commands
{
    public class UpsertRegistrationCommand
    {
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
