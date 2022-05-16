namespace ShelterHelper.Api.Features.Profiles.ViewProfile
{
    public interface IViewProfileQueryHandler
    {
        public Task<ProfileAndCurrentShelterBookingDto> HandleAsync(string identityId, CancellationToken cancellationToken);
        public Task<ProfileAndCurrentShelterBookingDto> HandleAsync(int userId, CancellationToken cancellationToken);
    }
}
