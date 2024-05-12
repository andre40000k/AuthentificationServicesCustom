using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Application.Request;
using Authentification.Application.Respons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RereshTokenController : ControllerBase
    {
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromServices] IRequestHendler<RefreshTokenCommand, TokenResponse> tokenCommand,
            [FromBody] TokenApiRequest tokenApiRequest)
        {
            try
            {
                var tokens = await tokenCommand.HendlerAsync(new RefreshTokenCommand
                {
                    AccessToken = tokenApiRequest.AccessToken,
                    RefreshToken = tokenApiRequest.RefreshToken
                });

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoketoken([FromServices] IRequestHendler<RevokeTokenCommand, bool> tokenCommand,
            [FromBody] TokenApiRequest tokenApiRequest)
        {
            try
            {
                var tokens = await tokenCommand.HendlerAsync(new RevokeTokenCommand
                {
                    AccessToken = tokenApiRequest.AccessToken,
                    RefreshToken = tokenApiRequest.RefreshToken
                });

                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
