namespace ShelterHelper.Api.Features.Bookings.ShelterCheckIn
{
    public record ShelterCheckInCommand
    {
        public ShelterCheckInCommand(int shelterId, int rentalDays)
        {
            ShelterId = shelterId;
            RentalDays = rentalDays;
        }

        public int ShelterId { get; init; }
        public int RentalDays { get; init; }
    }
}
