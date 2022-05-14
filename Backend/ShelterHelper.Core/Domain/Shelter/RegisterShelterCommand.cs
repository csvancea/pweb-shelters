using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.Shelter
{
    public record RegisterShelterCommand : ICreateAggregateCommand
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string MapsLink { get; init; }
        public int Capacity { get; init; }
        public int MaximumDaysForRental { get; init; }
        public int NumberOfUsers { get; init; }
        public bool Accessibility { get; init; }
        public bool Pets { get; init; }

        public RegisterShelterCommand(string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets)
        {
            Name = name;
            Address = address;
            MapsLink = mapsLink;
            Capacity = capacity;
            MaximumDaysForRental = maximumDaysForRental;
            NumberOfUsers = numberOfUsers;
            Accessibility = accessibility;
            Pets = pets;
        }
    }
}
