namespace ShelterHelper.Api.Features.Shelter.ViewAllShelters
{
    public interface IViewAllSheltersQueryHandler
    {
        public Task<IEnumerable<ShelterDto>> HandleAsync(CancellationToken cancellationToken);
    }
}
