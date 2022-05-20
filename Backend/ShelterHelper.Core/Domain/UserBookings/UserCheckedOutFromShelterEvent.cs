namespace ShelterHelper.Core.Domain.UserBookings
{
    public record UserCheckedOutFromShelterEvent
    {
        public int ShelterId { get; init; }
        public UserCheckedOutFromShelterEvent(int shelterId)
        {
            ShelterId = shelterId;   
        }
    }
}
