using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.Domain.UserBookings;
using MediatR;
using System.Net;

namespace ShelterHelper.Api.Features.Shelter.DecreaseShelterUsers
{
    public class UserCheckedOutFromShelterEventHandler : INotificationHandler<UserCheckedOutFromShelterEvent>
    {
        private readonly ISheltersRepository sheltersRepository;

        public UserCheckedOutFromShelterEventHandler(ISheltersRepository sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public async Task Handle(UserCheckedOutFromShelterEvent notification, CancellationToken cancellationToken)
        {
            var shelter = await sheltersRepository.GetAsync(notification.ShelterId, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {notification.ShelterId} not found!");
            }

            shelter.DecreaseShelterUsers();

            await sheltersRepository.SaveAsync(cancellationToken);
        }
    }
}
