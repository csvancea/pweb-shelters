namespace ShelterHelper.Api.Features.Bookings.ShelterCheckIn
{
    public interface IShelterCheckInCommandHandler
    {
        public Task HandleAsync(ShelterCheckInCommand command, string identityId, CancellationToken cancellationToken);
    }
}
