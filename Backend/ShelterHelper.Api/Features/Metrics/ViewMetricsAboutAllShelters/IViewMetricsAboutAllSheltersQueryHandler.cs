namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public interface IViewMetricsAboutAllSheltersQueryHandler
    {
        public Task<ViewMetricsAboutAllSheltersDto> HandleAsync(CancellationToken cancellationToken);
    }
}
