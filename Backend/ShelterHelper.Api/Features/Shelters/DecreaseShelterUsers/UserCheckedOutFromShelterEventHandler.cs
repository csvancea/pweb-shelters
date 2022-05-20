using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.Domain.UserBookings;
using MassTransit;
using System.Net;

namespace ShelterHelper.Api.Features.Shelters.DecreaseShelterUsers
{
    public class UserCheckedOutFromShelterEventHandler : IConsumer<UserCheckedOutFromShelterEvent>
    {
        private readonly ISheltersRepository sheltersRepository;

        public UserCheckedOutFromShelterEventHandler(ISheltersRepository sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public async Task Consume(ConsumeContext<UserCheckedOutFromShelterEvent> notification)
        {
            var shelter = await sheltersRepository.GetAsync(notification.Message.ShelterId, notification.CancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {notification.Message.ShelterId} not found!");
            }

            shelter.DecreaseShelterUsers();

            await sheltersRepository.SaveAsync(notification.CancellationToken);
        }
    }
}
