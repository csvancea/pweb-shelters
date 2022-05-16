namespace ShelterHelper.Api.Features.Shelters.DeleteShelter
{
    public interface IDeleteShelterHandler
    {
        public Task HandleAsync(int id, CancellationToken cancellation);
    }
}
