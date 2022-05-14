using ShelterHelper.Api.Authorization;
using ShelterHelper.Api.Features.Bookings.ShelterCheckIn;
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
        private readonly IShelterCheckOutCommandHandler shelterCheckOutCommandHandler;
        private readonly IViewBookingsQueryHandler viewBookingsQueryHandler;

        public BookingsController(IShelterCheckInCommandHandler shelterCheckInCommandHandler, IShelterCheckOutCommandHandler shelterCheckOutCommandHandler, IViewBookingsQueryHandler viewBookingsQueryHandler)
        {
            this.shelterCheckInCommandHandler = shelterCheckInCommandHandler;
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

        [HttpPut("checkOut/{id}")]
        [Authorize]
        public async Task<IActionResult> CheckOutAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var identityId = User.GetUserIdentityId();

            if (identityId == null)
            {
                return Unauthorized();
            }

            await shelterCheckOutCommandHandler.HandleAsync(id, identityId, cancellationToken);

            return NoContent();
        }

        [HttpGet("viewMyBookingHistory")]
        [Authorize]
        // TODO: RentalHistoryDto, rentals
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
    }
}
