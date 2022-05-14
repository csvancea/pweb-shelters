using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;
using MediatR;
using System.Net;

namespace ShelterHelper.Api.Features.Bookings.ShelterCheckOut
{
    public class ShelterCheckOutCommandHandler : IShelterCheckOutCommandHandler
    {
        private readonly IUsersBookingsRepository usersBookingsRepository;
        private readonly IMediator mediator;

        public ShelterCheckOutCommandHandler(IUsersBookingsRepository usersBookingsRepository, IMediator mediator)
        {
            this.usersBookingsRepository = usersBookingsRepository;
            this.mediator = mediator;
        }

        public async Task HandleAsync(int shelterId, string identityId, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            try
            {
                var userCheckedOutFromShelterEvent = user.CheckOutShelter(shelterId);

                await mediator.Publish(userCheckedOutFromShelterEvent, cancellationToken);

                await usersBookingsRepository.SaveAsync(cancellationToken);
            } 
            catch (BookingNotFoundException ex)
            {
                throw new ApiException(HttpStatusCode.NotFound, ex.Message);
            }   
        }
    }
}
