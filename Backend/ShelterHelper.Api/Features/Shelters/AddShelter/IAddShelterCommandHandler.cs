using ShelterHelper.Core.Domain;

namespace ShelterHelper.Api.Features.Shelters.AddShelter
{
    public interface IAddShelterCommandHandler
    {
        public Task HandleAsync(AddShelterCommand command, CancellationToken cancellation);
    }
}
