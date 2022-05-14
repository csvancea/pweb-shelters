using ShelterHelper.Core.Domain;

namespace ShelterHelper.Api.Features.Shelter.AddShelter
{
    public interface IAddShelterCommandHandler
    {
        public Task HandleAsync(AddShelterCommand command, CancellationToken cancellation);
    }
}
