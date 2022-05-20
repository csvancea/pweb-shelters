namespace ShelterHelper.Core.Domain.UserBookings
{
    public record UserBookedShelterEvent
    {
        public int ShelterId { get; init; }
        public int UserId { get; init; }
        public string? UserName { get; init; }
        public string UserEmail { get; init; }
        public int NumberOfDays { get; init; }

        public UserBookedShelterEvent(int shelterId, int userId, string? userName, string userEmail, int numberOfDays)
        {
            ShelterId = shelterId;
            UserId = userId;
            UserName = userName;
            UserEmail = userEmail;
            NumberOfDays = numberOfDays;
        }
    }
}
