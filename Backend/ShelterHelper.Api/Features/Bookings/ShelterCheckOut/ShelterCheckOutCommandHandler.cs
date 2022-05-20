using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;
using MassTransit;
using System.Net;

namespace ShelterHelper.Api.Features.Bookings.ShelterCheckOut
{
    public class ShelterCheckOutCommandHandler : IShelterCheckOutCommandHandler
    {
        private readonly IUsersBookingsRepository usersBookingsRepository;
        private readonly IBus mediator;

        public ShelterCheckOutCommandHandler(IUsersBookingsRepository usersBookingsRepository, IBus mediator)
        {
            this.usersBookingsRepository = usersBookingsRepository;
            this.mediator = mediator;
        }

        public async Task HandleAsync(ShelterCheckOutCommand command, string identityId, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            await HandleAsyncImpl(command, user, cancellationToken);
        }

        public async Task HandleAsync(ShelterCheckOutCommand command, int userId, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetAsync(userId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with userId {userId} does not have a registered profile");
            }

            await HandleAsyncImpl(command, user, cancellationToken);
        }

        private async Task HandleAsyncImpl(ShelterCheckOutCommand command, UsersBookingsDomain user, CancellationToken cancellationToken)
        {
            try
            {
                var userCheckedOutFromShelterEvent = user.CheckOutShelter(command.ShelterId);

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
