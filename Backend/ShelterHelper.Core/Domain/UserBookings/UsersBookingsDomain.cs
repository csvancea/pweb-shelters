using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public class UsersBookingsDomain : DomainOfAggregate<Users>
    {
        public UsersBookingsDomain(Users aggregate) : base(aggregate)
        {
        }

        public void UpdateProfile(string name, string email, string phone, string address, DateTime birthDate)
        {
            aggregate.Name = name;
            aggregate.Email = email;
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

            return new UserBookedShelterEvent(shelterId);
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

        public int GetProfileId() => aggregate.Id;
    }
}
