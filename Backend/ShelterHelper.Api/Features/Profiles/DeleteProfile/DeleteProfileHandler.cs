using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;

namespace ShelterHelper.Api.Features.Profiles.DeleteProfile
{
    public class DeleteProfileHandler : IDeleteProfileHandler
    {
        private readonly IUsersBookingsRepository userBookingsRepository;

        public DeleteProfileHandler(IUsersBookingsRepository userBookingsRepository)
        {
            this.userBookingsRepository = userBookingsRepository;
        }

        public async Task HandleAsync(int id, CancellationToken cancellationToken)
        {
            var user = await userBookingsRepository.GetAsync(id, cancellationToken) as UsersBookingsDomain;

            if (user == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, $"User with id {id} was not found.");
            }

            if (user.ProfileCanBeDeleted())
            {
                await userBookingsRepository.DeleteUserAsync(id, cancellationToken);
                await userBookingsRepository.SaveAsync(cancellationToken);
            } else
            {
                throw new ApiException(System.Net.HttpStatusCode.Conflict, $"User with id {id} cannot be deleted. It is probably booked!");
            }
        }
    }
}
