using ShelterHelper.Api.Features.Bookings.ShelterCheckIn;

namespace ShelterHelper.Api.Features.Bookings.ShelterCheckInExtend
{
    public interface IShelterCheckInExtendCommandHandler
    {
        public Task HandleAsync(ShelterCheckInCommand command, string identityId, CancellationToken cancellationToken);
        public Task HandleAsync(ShelterCheckInCommand command, int userId, CancellationToken cancellationToken);
    }
}
