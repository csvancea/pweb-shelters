namespace ShelterHelper.Api.Features.Profiles.DeleteProfile
{
    public interface IDeleteProfileHandler
    {
        public Task HandleAsync(int id, CancellationToken cancellation);
    }
}
