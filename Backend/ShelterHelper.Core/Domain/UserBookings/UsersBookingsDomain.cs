using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public class UsersBookingsDomain : DomainOfAggregate<Users>
    {
        private const int MaxDaysBeforeExtending = 100;

        public UsersBookingsDomain(Users aggregate) : base(aggregate)
        {
        }

        public void UpdateProfile(string? name, string? phone, string? address, DateTime? birthDate)
        {
            aggregate.Name = name;
            aggregate.PhoneNumber = phone;
            aggregate.Address = address;
            aggregate.BirthDate = birthDate;
        }

        public UserBookedShelterEvent CheckInShelter(int shelterId, int rentalDays)
        {
            if (aggregate.Bookings.Where(x => !x.ActualCheckOutDate.HasValue).Count() >= 1)
            {
                throw new UserAlreadyBookedException();
            }

            aggregate.Bookings.Add(new Bookings(shelterId, aggregate.Id, DateTime.Now, DateTime.Now.AddDays(rentalDays)));

            return new UserBookedShelterEvent(shelterId, aggregate.Id, aggregate.Name, aggregate.Email, rentalDays);
        }

        public void CheckInExtendShelter(int shelterId, int rentalDays)
        {
            var booking = aggregate.Bookings
              .FirstOrDefault(x => x.ShelterId == shelterId && !x.ActualCheckOutDate.HasValue);

            if (booking == null)
            {
                throw new BookingNotFoundException(shelterId);
            }

            if ((booking.ExpectedCheckOutDate - DateTime.Now) > TimeSpan.FromDays(MaxDaysBeforeExtending))
            {
                throw new BookingExtendTooSoon(MaxDaysBeforeExtending);
            }

            booking.ExpectedCheckOutDate += TimeSpan.FromDays(rentalDays);
        }

        public UserCheckedOutFromShelterEvent CheckOutShelter(int shelterId)
        {
            var booking = aggregate.Bookings
                .FirstOrDefault(x => x.ShelterId == shelterId && !x.ActualCheckOutDate.HasValue);

            if (booking == null)
            {
                throw new BookingNotFoundException(shelterId);
            }

            booking.ActualCheckOutDate = DateTime.Now;
            
            return new UserCheckedOutFromShelterEvent(shelterId);
        }

        public bool ProfileCanBeDeleted() => aggregate.Bookings.Count((b) => !b.ActualCheckOutDate.HasValue) == 0;

        public int GetProfileId() => aggregate.Id;
    }
}
