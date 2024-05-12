using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Application.Respons;
using Authentification.Services.WorkWithTokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Authentification.Services.Commands
{
    public class RefreshTokenCommandHendler : BaseTokenApi, IRequestHendler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IRepositories _repositories;

        public RefreshTokenCommandHendler(IRepositories repositories, IConfiguration configuration): base(configuration) 
        {
            _repositories = repositories;
        }

        public async Task<TokenResponse> HendlerAsync(RefreshTokenCommand request, CancellationToken cancellationToken = default)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);

            var result = Guid.TryParse(principal.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (!result) throw new Exception("Error");

            var token = await _repositories.GetTokenByUserIdAsync(userId);

            if (token is null || token.Token != request.RefreshToken || token.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("Invalid client request");

            var newAccessToken = await GenerateAccessTokenAsync(principal.Claims);
            var newRefreshToken = await GenerateRefreshTokenAsync();

            token.Token = newRefreshToken;

            await _repositories.UpdateEntityToDbAsync(token);

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

        }
    }
}
