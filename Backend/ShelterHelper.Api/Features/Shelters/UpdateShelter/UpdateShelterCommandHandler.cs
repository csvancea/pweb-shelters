using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;
using System.Net;

namespace ShelterHelper.Api.Features.Shelters.UpdateShelter
{
    public class UpdateShelterCommandHandler : IUpdateShelterCommandHandler
    {
        private readonly ISheltersRepository sheltersRepository;

        public UpdateShelterCommandHandler(ISheltersRepository sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public async Task HandleAsync(int shelterId, UpdateShelterCommand command, CancellationToken cancellationToken)
        {
            var shelter = await sheltersRepository.GetAsync(shelterId, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Shelter with id {shelterId} not found!");
            }

            shelter.UpdateShelter(command.Name, command.Address, command.MapsLink, command.Capacity, command.MaximumDaysForRental,
                command.NumberOfUsers, command.Accessibility, command.Pets);

            await sheltersRepository.SaveAsync(cancellationToken);
        }
    }
    
}
