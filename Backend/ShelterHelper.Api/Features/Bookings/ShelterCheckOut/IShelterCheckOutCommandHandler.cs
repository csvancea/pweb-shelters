namespace ShelterHelper.Api.Features.Bookings.ShelterCheckOut
{
    public interface IShelterCheckOutCommandHandler
    {
        public Task HandleAsync(int shelterId, string identityId, CancellationToken cancellationToken);
    }
}
