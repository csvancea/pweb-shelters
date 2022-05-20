using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.Domain.UserBookings;
using MassTransit;
using System.Net;

namespace ShelterHelper.Api.Features.Bookings.ShelterCheckIn
{
    public class ShelterCheckInCommandHandler : IShelterCheckInCommandHandler
    {
        private readonly ISheltersRepository sheltersRepository;
        private readonly IUsersBookingsRepository usersBookingsRepository;
        private readonly IBus mediator;

        public ShelterCheckInCommandHandler(ISheltersRepository sheltersRepository, IUsersBookingsRepository usersBookingsRepository, IBus mediator)
        {
            this.sheltersRepository = sheltersRepository;
            this.usersBookingsRepository = usersBookingsRepository;
            this.mediator = mediator;
        }

        public async Task HandleAsync(ShelterCheckInCommand command, string identityId, CancellationToken cancellationToken)
        {
            var shelter = await sheltersRepository.GetAsync(command.ShelterId, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {command.ShelterId} not found!");
            }

            if (!shelter.ShelterCanBeBooked(command.RentalDays))
            {
                throw new ApiException(HttpStatusCode.Conflict, $"Shelter with id {command.ShelterId} cannot be booked!");
            }

            var user = await usersBookingsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            try
            {
                var userBookedShelterEvent = user.CheckInShelter(command.ShelterId, command.RentalDays);
                await mediator.Publish(userBookedShelterEvent, cancellationToken);

                await usersBookingsRepository.SaveAsync(cancellationToken);
            } 
            catch (UserAlreadyBookedException ex)
            {
                throw new ApiException(HttpStatusCode.Conflict, ex.Message);
            }
        }
    }
}
