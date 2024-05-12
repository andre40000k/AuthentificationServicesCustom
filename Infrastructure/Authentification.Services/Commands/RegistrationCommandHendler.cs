using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Application.Respons;
using Authentification.Domain.Entities;

namespace Authentification.Services.Commands
{
    public class RegistrationCommandHendler : IRequestHendler<UpsertRegistrationCommand, RegistrationResponse>
    {
        private readonly IRepositories _repository;

        public RegistrationCommandHendler(IRepositories repository)
        {
            _repository = repository;
        }
        public async Task<RegistrationResponse> HendlerAsync(UpsertRegistrationCommand request, CancellationToken cancellationToken = default)
        {
            var currentUser = _repository.GetUserByEmailAsync(request.UserEmail);

            if (currentUser != null) throw new Exception("User already exists");

            User user = new User()
            {
                UserEmail = request.UserEmail,
                UserFirstName = request.UserFirstName,
                UserSecondName = request.UserSecondName,
                UserPassword = request.UserPassword,
            };


            await _repository.AddEntityToDbAsync(user);

            return new RegistrationResponse()
            {
                StatusCode = "500"
            };
        }
    }
}
