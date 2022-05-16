namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public interface IViewMetricsAboutSingleShelterQueryHandler
    {
        public Task<ViewMetricsAboutShelterDto> HandleAsync(int shelterId, CancellationToken cancellationToken);
    }
}
