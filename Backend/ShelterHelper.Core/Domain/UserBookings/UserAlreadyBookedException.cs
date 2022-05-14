namespace ShelterHelper.Core.Domain.UserBookings
{
    public class UserAlreadyBookedException : Exception
    {
        public UserAlreadyBookedException() : base("Current user has too many rentals at this moment!")
        {
        }
    }
}
