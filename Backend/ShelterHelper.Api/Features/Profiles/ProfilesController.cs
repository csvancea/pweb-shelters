using ShelterHelper.Api.Authorization;
using ShelterHelper.Api.Features.Profiles.RegisterProfile;
using ShelterHelper.Api.Features.Profiles.UpdateProfile;
using ShelterHelper.Api.Features.Profiles.ViewProfile;
using ShelterHelper.Api.Features.Profiles.ViewAllProfiles;
using ShelterHelper.Api.Features.Profiles.DeleteProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShelterHelper.Api.Features.Profiles
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IRegisterProfileCommandHandler registerProfileCommandHandler;
        private readonly IUpdateProfileCommandHandler updateProfileCommandHandler;
        private readonly IViewProfileQueryHandler viewProfileQueryHandler;
        private readonly IViewAllProfilesQueryHandler viewAllProfilesQueryHandler;
        private readonly IDeleteProfileHandler deleteProfileHandler;

        public ProfilesController(IRegisterProfileCommandHandler registerProfileCommandHandler, IUpdateProfileCommandHandler updateProfileCommandHandler, IViewProfileQueryHandler viewProfileQueryHandler, IViewAllProfilesQueryHandler viewAllProfilesQueryHandler, IDeleteProfileHandler deleteProfileHandler)
        {
            this.registerProfileCommandHandler = registerProfileCommandHandler;
            this.updateProfileCommandHandler = updateProfileCommandHandler;
            this.viewProfileQueryHandler = viewProfileQueryHandler;
            this.viewAllProfilesQueryHandler = viewAllProfilesQueryHandler;
            this.deleteProfileHandler = deleteProfileHandler;
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

        [HttpPut("updateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await updateProfileCommandHandler.HandleAsync(identityId, command, cancellationToken);

            return Ok();
        }

        [HttpPut("updateProfile/{id}")]
        [Authorize("AdminAccess")]
        public async Task<IActionResult> UpdateProfileAsync([FromRoute] int id, [FromBody] UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            await updateProfileCommandHandler.HandleAsync(id, command, cancellationToken);

            return Ok();
        }

        [HttpGet("viewProfile")]
        [Authorize]
        public async Task<ActionResult<ProfileAndCurrentShelterBookingDto>> ViewProfileAsync(CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            var profile = await viewProfileQueryHandler.HandleAsync(identityId, cancellationToken);

            return Ok(profile);
        }

        [HttpGet("viewProfile/{id}")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ProfileAndCurrentShelterBookingDto>> ViewProfileAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var profile = await viewProfileQueryHandler.HandleAsync(id, cancellationToken);

            return Ok(profile);
        }

        [HttpGet("viewAllProfiles")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ProfileAndCurrentShelterBookingDto>> ViewAllProfilesAsync(CancellationToken cancellationToken)
        {
            var profiles = await viewAllProfilesQueryHandler.HandleAsync(cancellationToken);

            return Ok(profiles);
        }

        [HttpDelete("deleteProfile/{id}")]
        [Authorize("AdminAccess")]
        public async Task<IActionResult> DeleteProfileAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await deleteProfileHandler.HandleAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
