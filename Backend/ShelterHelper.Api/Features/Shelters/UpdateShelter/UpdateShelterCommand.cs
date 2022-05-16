using ShelterHelper.Core.Domain.Shelter;

namespace ShelterHelper.Api.Features.Shelters.UpdateShelter
{
    public record UpdateShelterCommand : RegisterShelterCommand
    {
        public UpdateShelterCommand(string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers = 0, bool accessibility = false, bool pets = false) : base(name, address, mapsLink, capacity, maximumDaysForRental, numberOfUsers, accessibility, pets)
        {
        }
    }
}
