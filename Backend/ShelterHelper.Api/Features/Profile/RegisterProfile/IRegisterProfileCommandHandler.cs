namespace ShelterHelper.Api.Features.Profile.RegisterProfile
{
    public interface IRegisterProfileCommandHandler
    {
        public Task HandleAsync(RegisterProfileCommand command, string identityId, CancellationToken cancellationToken);
    }
}
