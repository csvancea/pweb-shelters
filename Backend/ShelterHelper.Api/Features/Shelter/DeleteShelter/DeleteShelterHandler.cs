using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.Shelter;

namespace ShelterHelper.Api.Features.Shelter.DeleteShelter
{
    public class DeleteShelterHandler : IDeleteShelterHandler
    {
        private readonly ISheltersRepository shelterRepository;

        public DeleteShelterHandler(ISheltersRepository shelterRepository)
        {
            this.shelterRepository = shelterRepository;
        }

        public async Task HandleAsync(int id, CancellationToken cancellationToken)
        {
            var shelter = await shelterRepository.GetAsync(id, cancellationToken) as SheltersDomain;

            if (shelter == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.NotFound, $"Shelter with id {id} was not found.");
            }

            if (shelter.ShelterCanBeDeleted())
            {
                await shelterRepository.DeleteShelterAsync(id, cancellationToken);
                await shelterRepository.SaveAsync(cancellationToken);
            } else
            {
                throw new ApiException(System.Net.HttpStatusCode.Conflict, $"Shelter with id {id} cannot be deleted. It is probably booked!");
            }
        }
    }
}
