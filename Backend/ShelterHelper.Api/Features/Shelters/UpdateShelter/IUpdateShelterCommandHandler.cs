using ShelterHelper.Core.Domain;

namespace ShelterHelper.Api.Features.Shelters.UpdateShelter
{
    public interface IUpdateShelterCommandHandler
    {
        public Task HandleAsync(int shelterId, UpdateShelterCommand command, CancellationToken cancellation);
    }
}
