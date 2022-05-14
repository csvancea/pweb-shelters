using ShelterHelper.Api.Authorization;
using ShelterHelper.Api.Features.Profile.RegisterProfile;
using ShelterHelper.Api.Features.Profile.ViewProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShelterHelper.Api.Features.Profile
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IRegisterProfileCommandHandler registerProfileCommandHandler;
        private readonly IViewProfileQueryHandler viewProfileQueryHandler;

        public ProfilesController(IRegisterProfileCommandHandler registerProfileCommandHandler, IViewProfileQueryHandler viewProfileQueryHandler)
        {
            this.registerProfileCommandHandler = registerProfileCommandHandler;
            this.viewProfileQueryHandler = viewProfileQueryHandler;
        }

        [HttpPost("registerProfile")]
        [Authorize]
        public async Task<IActionResult> RegisterProfileAsync([FromBody] RegisterProfileCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await registerProfileCommandHandler.HandleAsync(command, identityId, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("viewProfile")]
        [Authorize]
        public async Task<ActionResult<ProfileDto>> ViewProfileAsync(CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            var profile = await viewProfileQueryHandler.HandleAsync(identityId, cancellationToken);

            return Ok(profile);
        }
    }
}
