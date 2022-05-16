using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.DataModel
{
    public class Shelters : Entity, IAggregateRoot
    {
        public Shelters(string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets)
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

        public string Name { get; set; }
        public string Address { get; set; }
        public string MapsLink { get; set; }
        public int Capacity { get; set; }
        public int MaximumDaysForRental { get; set; }
        public int NumberOfUsers { get; set; }
        public bool Accessibility { get; set; }
        public bool Pets { get; set; }
    }
}