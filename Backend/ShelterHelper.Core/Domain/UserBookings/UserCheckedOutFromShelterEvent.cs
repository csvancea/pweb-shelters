using MediatR;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public record UserCheckedOutFromShelterEvent : INotification
    {
        public int ShelterId { get; private set; }
        public UserCheckedOutFromShelterEvent(int shelterId)
        {
            ShelterId = shelterId;   
        }
    }
}
