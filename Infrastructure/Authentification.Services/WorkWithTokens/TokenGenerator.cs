using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Authentification.Services.WorkWithTokens
{
    public abstract class TokenGenerator
    {
        private readonly IConfiguration _configuration;

        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
        {
            var authSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["JWTKey:Secret"]));
            var _tokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenHendler = new JwtSecurityTokenHandler();
            var tokenDeskription = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWTKey:ValidAudience"],
                Issuer = _configuration["JWTKey:ValidIssure"],
                Expires = DateTime.UtcNow.AddHours(_tokenExpiryTimeInHour),
                SigningCredentials = new SigningCredentials(authSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims),
            };

            var securityToken = tokenHendler.CreateToken(tokenDeskription);

            return await Task.Run(() => tokenHendler.WriteToken(securityToken));
        }

        public static async Task<string> GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            await Task.Run(() => rng.GetBytes(randomNumber));

            return Convert.ToBase64String(randomNumber);
        }
    }
}
