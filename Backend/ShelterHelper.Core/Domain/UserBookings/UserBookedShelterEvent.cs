using MediatR;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public record UserBookedShelterEvent : INotification
    {
        public int ShelterId { get; private set; }
        public UserBookedShelterEvent(int shelterId)
        {
            ShelterId = shelterId;
        }
    }
}
