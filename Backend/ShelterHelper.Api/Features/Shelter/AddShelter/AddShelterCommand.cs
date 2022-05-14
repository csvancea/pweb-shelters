using ShelterHelper.Core.Domain.Shelter;

namespace ShelterHelper.Api.Features.Shelter.AddShelter
{
    public record AddShelterCommand : RegisterShelterCommand
    {
        public AddShelterCommand(string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets) : base(name, address, mapsLink, capacity, maximumDaysForRental, numberOfUsers, accessibility, pets)
        {
        }
    }
}
