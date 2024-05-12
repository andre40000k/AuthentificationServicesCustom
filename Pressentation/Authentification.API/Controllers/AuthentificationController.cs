using Authentification.Application.Commands;
using Authentification.Application.Interfaces;
using Authentification.Application.Request;
using Authentification.Application.Respons;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromServices] IRequestHendler<UpsertRegistrationCommand, RegistrationResponse> registrationCommand,
            [FromBody] RegistrationRequest upsertRegistrationRequest)
        {
            var result = await registrationCommand.HendlerAsync(new UpsertRegistrationCommand()
            {
                UserEmail = upsertRegistrationRequest.UserEmail,
                UserFirstName = upsertRegistrationRequest.UserFirstName,
                UserSecondName = upsertRegistrationRequest.UserSecondName,
                UserPassword = upsertRegistrationRequest.UserPassword
            });

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromServices] IRequestHendler<LoginCommand, LoginResponse> loginCommand,
            [FromBody] LoginRequest loginRequest)
        {
            try
            {
                var user = await loginCommand.HendlerAsync(new LoginCommand
                {
                    UserEmail = loginRequest.UserEmail,
                    UserPassword = loginRequest.UserPassword
                });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }
    }
}
