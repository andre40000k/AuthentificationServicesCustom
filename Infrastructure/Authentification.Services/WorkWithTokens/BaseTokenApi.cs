using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentification.Services.WorkWithTokens
{
    public abstract class BaseTokenApi
    {
        private readonly IConfiguration _configuration;

        public BaseTokenApi(IConfiguration configuration)
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

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
