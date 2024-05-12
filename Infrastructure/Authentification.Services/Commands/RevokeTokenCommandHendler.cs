using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Services.WorkWithTokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Authentification.Services.Commands
{
    public class RevokeTokenCommandHendler : BaseTokenApi, IRequestHendler<RevokeTokenCommand, bool>
    {
        private readonly IRepositories _repositories;

        public RevokeTokenCommandHendler(IRepositories repositories, IConfiguration configuration) : base(configuration) 
        {
            _repositories = repositories;
        }

        public async Task<bool> HendlerAsync(RevokeTokenCommand request, CancellationToken cancellationToken = default)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var result = Guid.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (!result) throw new Exception("Error");

            var token = await _repositories.GetTokenByUserIdAsync(userId) ?? throw new Exception("Invalid userId"); ;

            await _repositories.DeleteTokenAsync(token);

            return true;
        }
    }
}
