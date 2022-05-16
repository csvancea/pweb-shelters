namespace ShelterHelper.Api.Features.Profiles.UpdateProfile
{
    public interface IUpdateProfileCommandHandler
    {
        public Task HandleAsync(string identityId, UpdateProfileCommand command, CancellationToken cancellation);
        public Task HandleAsync(int userId, UpdateProfileCommand command, CancellationToken cancellation);
    }
}
