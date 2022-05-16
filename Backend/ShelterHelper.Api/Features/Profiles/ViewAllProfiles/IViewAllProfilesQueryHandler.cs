using ShelterHelper.Api.Features.Profiles.ViewProfile;

namespace ShelterHelper.Api.Features.Profiles.ViewAllProfiles
{
    public interface IViewAllProfilesQueryHandler
    {
        public Task<IEnumerable<ProfileAndCurrentShelterBookingDto>> HandleAsync(CancellationToken cancellationToken);
    }
}
