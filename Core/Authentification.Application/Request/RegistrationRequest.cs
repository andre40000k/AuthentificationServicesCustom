using Authentification.Domain.Common;

namespace Authentification.Application.Request
{
    public class RegistrationRequest : BaseRequest
    {
        public string UserFirstName { get; set; }
        public string UserSecondName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
