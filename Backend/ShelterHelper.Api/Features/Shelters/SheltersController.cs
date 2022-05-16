using ShelterHelper.Api.Features.Shelters.AddShelter;
using ShelterHelper.Api.Features.Shelters.UpdateShelter;
using ShelterHelper.Api.Features.Shelters.DeleteShelter;
using ShelterHelper.Api.Features.Shelters.ViewAllShelters;
using ShelterHelper.Api.Features.Shelters.ViewStatusAboutShelter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShelterHelper.Api.Features.Shelters
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SheltersController : ControllerBase
    {
        private readonly IAddShelterCommandHandler addShelterCommandHandler;
        private readonly IUpdateShelterCommandHandler updateShelterCommandHandler;
        private readonly IViewAllSheltersQueryHandler viewAllSheltersQueryHandler;
        private readonly IViewStatusAboutShelterQueryHandler viewStatusAboutShelterQueryHandler;
        private readonly IDeleteShelterHandler deleteShelterHandler;

        public SheltersController(
            IAddShelterCommandHandler addShelterCommandHandler,
            IUpdateShelterCommandHandler updateShelterCommandHandler,
            IViewAllSheltersQueryHandler viewAllSheltersQueryHandler,
            IViewStatusAboutShelterQueryHandler viewStatusAboutShelterQueryHandler,
            IDeleteShelterHandler deleteShelterHandler)
        {
            this.addShelterCommandHandler = addShelterCommandHandler;
            this.updateShelterCommandHandler = updateShelterCommandHandler;
            this.viewAllSheltersQueryHandler = viewAllSheltersQueryHandler;
            this.viewStatusAboutShelterQueryHandler = viewStatusAboutShelterQueryHandler;
            this.deleteShelterHandler = deleteShelterHandler;
        }

        [HttpPost("addShelter")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> AddShelterAsync([FromBody] AddShelterCommand command, CancellationToken cancellationToken)
        {
            await addShelterCommandHandler.HandleAsync(command, cancellationToken);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("updateShelter/{id}")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> UpdateShelterAsync([FromRoute] int id, [FromBody] UpdateShelterCommand command, CancellationToken cancellationToken)
        {
            await updateShelterCommandHandler.HandleAsync(id, command, cancellationToken);

            return Ok();
        }

        [HttpGet("viewAllShelters")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ShelterDto>>> ViewAllSheltersAsync(CancellationToken cancellationToken)
        {
            var shelters = await viewAllSheltersQueryHandler.HandleAsync(cancellationToken);

            return Ok(shelters);
        }

        [HttpGet("viewStatusAboutShelter/{id}")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ShelterWithBookingHistoryDto>> ViewStatusAboutShelterAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var shelter = await viewStatusAboutShelterQueryHandler.HandleAsync(id, cancellationToken);

            return Ok(shelter);
        }

        [HttpDelete("deleteShelter/{id}")]
        [Authorize("AdminAccess")]
        public async Task<IActionResult> DeleteShelterAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await deleteShelterHandler.HandleAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
