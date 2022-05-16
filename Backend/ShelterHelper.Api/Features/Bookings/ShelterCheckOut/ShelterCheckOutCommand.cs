namespace ShelterHelper.Api.Features.Bookings.ShelterCheckOut
{
    public record ShelterCheckOutCommand
    {
        public ShelterCheckOutCommand(int shelterId)
        {
            ShelterId = shelterId;
        }

        public int ShelterId { get; init; }
    }
}
