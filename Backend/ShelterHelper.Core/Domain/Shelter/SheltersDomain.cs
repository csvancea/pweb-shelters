using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.Shelter
{
    public class SheltersDomain : DomainOfAggregate<Shelters>
    {
        public SheltersDomain(Shelters aggregate) : base(aggregate)
        {
        }

        public void UpdateShelter(string name, string address, string mapsLink, int capacity, int maximumDaysForRental, int numberOfUsers, bool accessibility, bool pets)
        {
            aggregate.Name = name;
            aggregate.Address = address;
            aggregate.MapsLink = mapsLink;
            aggregate.Capacity = capacity;
            aggregate.MaximumDaysForRental = maximumDaysForRental;
            aggregate.NumberOfUsers = numberOfUsers;
            aggregate.Accessibility = accessibility;
            aggregate.Pets = pets;
        }

        public bool ShelterCanBeBooked(int rentalDays) => aggregate.NumberOfUsers < aggregate.Capacity && rentalDays <= aggregate.MaximumDaysForRental;
        public void IncreaseShelterUsers() => aggregate.NumberOfUsers++;
        public void DecreaseShelterUsers() => aggregate.NumberOfUsers--;
        public bool ShelterCanBeDeleted() => aggregate.NumberOfUsers == 0;
    }
}
