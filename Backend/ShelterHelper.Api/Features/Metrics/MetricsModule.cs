using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter;
using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters;

namespace ShelterHelper.Api.Features.Metrics
{
    internal static class MetricsModule
    {
        internal static void AddMetricsHandlers(this IServiceCollection services)
        {
            services.AddTransient<IViewMetricsAboutSingleShelterQueryHandler, ViewMetricsAboutSingleShelterQueryHandler>();
            services.AddTransient<IViewMetricsAboutAllSheltersQueryHandler, ViewMetricsAboutAllSheltersQueryHandler>();
        }
    }
}
