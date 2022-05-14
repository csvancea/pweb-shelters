namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public interface IViewMetricsAboutSingleShelterQueryHandler
    {
        public Task<ViewMetricsAboutBookDto> HandleAsync(int shelterId, CancellationToken cancellationToken);
    }
}
