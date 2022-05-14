using ShelterHelper.Core.Domain.Shelter;

namespace ShelterHelper.Api.Features.Shelter.AddShelter
{
    public class AddShelterCommandHandler : IAddShelterCommandHandler
    {
        private readonly ISheltersRepository sheltersRepository;

        public AddShelterCommandHandler(ISheltersRepository sheltersRepository)
        {
            this.sheltersRepository = sheltersRepository;
        }

        public Task HandleAsync(AddShelterCommand command, CancellationToken cancellationToken)
            => sheltersRepository.AddAsync(
                new RegisterShelterCommand(command.Name, command.Address, command.MapsLink, command.Capacity, command.MaximumDaysForRental, command.NumberOfUsers, command.Accessibility, command.Pets), 
                cancellationToken);
    }
    
}
