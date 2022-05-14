using ShelterHelper.Core.DataModel;

namespace ShelterHelper.Api.Features.Shelter.ViewAllShelters
{
    public record ShelterDto
    {
        public ShelterDto(int id, string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets)
        {
            Id = id;
            Name = name;
            Address = address;
            MapsLink = mapsLink;
            Capacity = capacity;
            MaximumDaysForRental = maximumDaysForRental;
            NumberOfUsers = numberOfUsers;
            Accessibility = accessibility;
            Pets = pets;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string Address { get; init; }
        public string MapsLink { get; init; }
        public int Capacity { get; init; }
        public int MaximumDaysForRental { get; init; }
        public int NumberOfUsers { get; init; }
        public bool Accessibility { get; init; }
        public bool Pets { get; init; }
    }
}
