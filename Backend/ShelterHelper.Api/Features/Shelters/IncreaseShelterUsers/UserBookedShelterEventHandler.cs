using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.Domain.UserBookings;
using MediatR;
using System.Net;

namespace ShelterHelper.Api.Features.Shelters.IncreaseShelterUsers
{
    public class UserBookedShelterEventHandler : INotificationHandler<UserBookedShelterEvent>
    {
        private readonly ISheltersRepository sheltersRepository;

        public UserBookedShelterEventHandler(ISheltersRepository sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public async Task Handle(UserBookedShelterEvent notification, CancellationToken cancellationToken)
        {
            var shelter = await sheltersRepository.GetAsync(notification.ShelterId, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {notification.ShelterId} not found!");
            }

            shelter.IncreaseShelterUsers();

            await sheltersRepository.SaveAsync(cancellationToken);
        }
    }
}
