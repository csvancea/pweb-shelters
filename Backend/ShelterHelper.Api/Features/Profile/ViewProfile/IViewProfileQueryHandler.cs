namespace ShelterHelper.Api.Features.Profile.ViewProfile
{
    public interface IViewProfileQueryHandler
    {
        public Task<ProfileDto> HandleAsync(string identityId, CancellationToken cancellationToken);
    }
}
