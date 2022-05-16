using ShelterHelper.Api.Authorization;
using ShelterHelper.Api.Features.Bookings.ShelterCheckIn;
using ShelterHelper.Api.Features.Bookings.ShelterCheckInExtend;
using ShelterHelper.Api.Features.Bookings.ShelterCheckOut;
using ShelterHelper.Api.Features.Bookings.ViewBookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelterHelper.Api.Features.Bookings
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IShelterCheckInCommandHandler shelterCheckInCommandHandler;
        private readonly IShelterCheckInExtendCommandHandler shelterCheckInExtendCommandHandler;
        private readonly IShelterCheckOutCommandHandler shelterCheckOutCommandHandler;
        private readonly IViewBookingsQueryHandler viewBookingsQueryHandler;

        public BookingsController(IShelterCheckInCommandHandler shelterCheckInCommandHandler, IShelterCheckInExtendCommandHandler shelterCheckInExtendCommandHandler, IShelterCheckOutCommandHandler shelterCheckOutCommandHandler, IViewBookingsQueryHandler viewBookingsQueryHandler)
        {
            this.shelterCheckInCommandHandler = shelterCheckInCommandHandler;
            this.shelterCheckInExtendCommandHandler = shelterCheckInExtendCommandHandler;
            this.shelterCheckOutCommandHandler = shelterCheckOutCommandHandler;
            this.viewBookingsQueryHandler = viewBookingsQueryHandler;
        }

        [HttpPut("checkIn")]
        [Authorize]
        public async Task<IActionResult> CheckInAsync([FromBody] ShelterCheckInCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await shelterCheckInCommandHandler.HandleAsync(command, identityId, cancellationToken);

            return NoContent();
        }

        [HttpPut("checkInExtend")]
        [Authorize]
        public async Task<IActionResult> CheckInExtendAsync([FromBody] ShelterCheckInCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await shelterCheckInExtendCommandHandler.HandleAsync(command, identityId, cancellationToken);

            return Ok();
        }

        [HttpPut("checkInExtend/{id}")]
        [Authorize("AdminAccess")]
        public async Task<IActionResult> CheckInExtendAsync([FromRoute] int id, [FromBody] ShelterCheckInCommand command, CancellationToken cancellationToken)
        {
            await shelterCheckInExtendCommandHandler.HandleAsync(command, id, cancellationToken);

            return Ok();
        }

        [HttpPut("checkOut")]
        [Authorize]
        public async Task<IActionResult> CheckOutAsync([FromBody] ShelterCheckOutCommand command, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await shelterCheckOutCommandHandler.HandleAsync(command, identityId, cancellationToken);

            return NoContent();
        }

        [HttpPut("checkOut/{id}")]
        [Authorize("AdminAccess")]
        public async Task<IActionResult> CheckOutAsync([FromRoute] int id, [FromBody] ShelterCheckOutCommand command, CancellationToken cancellationToken)
        {
            await shelterCheckOutCommandHandler.HandleAsync(command, id, cancellationToken);

            return NoContent();
        }

        [HttpGet("viewBookingHistory")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<RentalHistoryDto>>> ViewBookingsAsync(CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            var rentals = await viewBookingsQueryHandler.HandleAsync(identityId, cancellationToken);

            return Ok(rentals);
        }

        [HttpGet("viewBookingHistory/{id}")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<IEnumerable<RentalHistoryDto>>> ViewBookingsAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var rentals = await viewBookingsQueryHandler.HandleAsync(id, cancellationToken);

            return Ok(rentals);
        }
    }
}
