using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.DataModel
{
    public class Bookings : Entity
    {
        public Bookings(int shelterId, int userId, DateTime checkInDate, DateTime expectedCheckOutDate, DateTime? actualCheckOutDate = null)
        {
            ShelterId = shelterId;
            UserId = userId;
            CheckInDate = checkInDate;
            ExpectedCheckOutDate = expectedCheckOutDate;
            ActualCheckOutDate = actualCheckOutDate;
        }

        public int ShelterId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime ExpectedCheckOutDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }

        public virtual Users User { get; set; } = null!;
    }
}
