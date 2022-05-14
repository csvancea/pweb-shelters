using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter;

namespace ShelterHelper.Api.Features.Metrics
{
    internal static class MetricsModule
    {
        internal static void AddMetricsHandlers(this IServiceCollection services)
        {
            services.AddTransient<IViewMetricsAboutSingleShelterQueryHandler, ViewMetricsAboutSingleShelterQueryHandler>();
        }
    }
}
