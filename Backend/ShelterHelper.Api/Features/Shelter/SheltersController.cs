using ShelterHelper.Api.Features.Shelter.AddShelter;
using ShelterHelper.Api.Features.Shelter.DeleteShelter;
using ShelterHelper.Api.Features.Shelter.ViewAllShelters;
using ShelterHelper.Api.Features.Shelter.ViewStatusAboutShelter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ShelterHelper.Api.Features.Shelter
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SheltersController : ControllerBase
    {
        private readonly IAddShelterCommandHandler addShelterCommandHandler;
        private readonly IViewAllSheltersQueryHandler viewAllSheltersQueryHandler;
        private readonly IViewStatusAboutShelterQueryHandler viewStatusAboutShelterQueryHandler;
        private readonly IDeleteShelterHandler deleteShelterHandler;

        public SheltersController(
            IAddShelterCommandHandler addShelterCommandHandler,
            IViewAllSheltersQueryHandler viewAllSheltersQueryHandler,
            IViewStatusAboutShelterQueryHandler viewStatusAboutShelterQueryHandler,
            IDeleteShelterHandler deleteShelterHandler)
        {
            this.addShelterCommandHandler = addShelterCommandHandler;
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
