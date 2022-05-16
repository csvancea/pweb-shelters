using ShelterHelper.Api.Features.Bookings.ShelterCheckIn;
using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.Domain.UserBookings;
using MediatR;
using System.Net;

namespace ShelterHelper.Api.Features.Bookings.ShelterCheckInExtend
{
    public class ShelterCheckInExtendCommandHandler : IShelterCheckInExtendCommandHandler
    {
        private readonly ISheltersRepository sheltersRepository;
        private readonly IUsersBookingsRepository usersBookingsRepository;

        public ShelterCheckInExtendCommandHandler(ISheltersRepository sheltersRepository, IUsersBookingsRepository usersBookingsRepository)
        {
            this.sheltersRepository = sheltersRepository;
            this.usersBookingsRepository = usersBookingsRepository;
        }

        public async Task HandleAsync(ShelterCheckInCommand command, string identityId, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            await HandleAsyncImpl(command, user, cancellationToken);
        }

        public async Task HandleAsync(ShelterCheckInCommand command, int id, CancellationToken cancellationToken)
        {
            var user = await usersBookingsRepository.GetAsync(id, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with id {id} does not have a registered profile");
            }

            await HandleAsyncImpl(command, user, cancellationToken);
        }

        private async Task HandleAsyncImpl(ShelterCheckInCommand command, UsersBookingsDomain user, CancellationToken cancellationToken)
        {
            var shelter = await sheltersRepository.GetAsync(command.ShelterId, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {command.ShelterId} not found!");
            }

            if (!shelter.ShelterCanBeExtended(command.RentalDays))
            {
                throw new ApiException(HttpStatusCode.Conflict, $"Shelter with id {command.ShelterId} cannot be extended!");
            }

            try
            {
                user.CheckInExtendShelter(command.ShelterId, command.RentalDays);
                await usersBookingsRepository.SaveAsync(cancellationToken);
            }
            catch (BookingNotFoundException ex)
            {
                throw new ApiException(HttpStatusCode.NotFound, ex.Message);
            }
            catch (BookingExtendTooSoon ex)
            {
                throw new ApiException(HttpStatusCode.Conflict, ex.Message);
            }
        }
    }
}
