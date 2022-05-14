namespace ShelterHelper.Core.Domain.UserBookings
{
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException(int shelterId) : base($"Booking for shelter {shelterId} either finished or not found!")
        {
        }
    }
}
