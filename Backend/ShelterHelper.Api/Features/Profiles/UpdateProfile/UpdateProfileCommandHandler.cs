using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;
using System.Net;

namespace ShelterHelper.Api.Features.Profiles.UpdateProfile
{
    public class UpdateProfileCommandHandler : IUpdateProfileCommandHandler
    {
        private readonly IUsersBookingsRepository userBookingsRepository;

        public UpdateProfileCommandHandler(IUsersBookingsRepository userBookingsRepository)
        {
            this.userBookingsRepository = userBookingsRepository;
        }

        public async Task HandleAsync(string identityId, UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var user = await userBookingsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"User with identity {identityId} not found!");
            }

            user.UpdateProfile(command.Name, command.PhoneNumber, command.Address, command.BirthDate);

            await HandleAsyncImpl(user, command, cancellationToken);
        }

        public async Task HandleAsync(int userId, UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var user = await userBookingsRepository.GetAsync(userId, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"User with id {userId} not found!");
            }

            await HandleAsyncImpl(user, command, cancellationToken);
        }

        private async Task HandleAsyncImpl(UsersBookingsDomain user, UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            user.UpdateProfile(command.Name, command.PhoneNumber, command.Address, command.BirthDate);

            await userBookingsRepository.SaveAsync(cancellationToken);
        }
    }
    
}
