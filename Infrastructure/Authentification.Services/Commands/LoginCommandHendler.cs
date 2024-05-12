using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Application.Respons;
using Authentification.Domain.Entities;
using Authentification.Services.WorkWithTokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authentification.Services.Commands
{
    public class LoginCommandHendler : BaseTokenApi, IRequestHendler<LoginCommand, LoginResponse>
    {
        private readonly IRepositories _repositories;
        private readonly IConfiguration _configuration;

        public LoginCommandHendler(IRepositories repositories, 
            IConfiguration configuration) : base(configuration) 
        {
            _repositories = repositories;
            _configuration = configuration;
        }

        public async Task<LoginResponse> HendlerAsync(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var currentUser = await _repositories.GetUserByEmailAsync(request.UserEmail) ?? throw new Exception("Invalid username or password");

            if(currentUser.UserPassword != request.UserPassword) throw new Exception("Invalid username or password");

            //Roles

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, Convert.ToString(currentUser.UserId)),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var accessToken = await GenerateAccessTokenAsync(authClaims);
            var refreshToken = await GenerateRefreshTokenAsync();

            var token = new RefreshToken
            {
                UserId = currentUser.UserId,
                Token = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToInt16(_configuration["JWTKey:RefreshTokenExpiryTimeInDay"])),
            };

            await _repositories.AddEntityToDbAsync(token);

            return new LoginResponse
            {
                UserEmail = currentUser.UserEmail,
                UserFirstName = currentUser.UserFirstName,
                TokenResponse = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }
    }
}
