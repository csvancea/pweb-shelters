namespace ShelterHelper.Api.Features.Shelter.DeleteShelter
{
    public interface IDeleteShelterHandler
    {
        public Task HandleAsync(int id, CancellationToken cancellation);
    }
}
