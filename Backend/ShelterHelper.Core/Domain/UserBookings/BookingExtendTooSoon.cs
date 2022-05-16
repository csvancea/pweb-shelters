namespace ShelterHelper.Core.Domain.UserBookings
{
    public class BookingExtendTooSoon : Exception
    {
        public BookingExtendTooSoon(int maxRenewDaysBefore) : base($"Booking can be extended only {maxRenewDaysBefore} day(s) before expected checkout date!")
        {
        }
    }
}
