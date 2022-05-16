namespace ShelterHelper.Api.Features.Bookings.ShelterCheckOut
{
    public interface IShelterCheckOutCommandHandler
    {
        public Task HandleAsync(ShelterCheckOutCommand command, string identityId, CancellationToken cancellationToken);
        public Task HandleAsync(ShelterCheckOutCommand command, int userId, CancellationToken cancellationToken);
    }
}
