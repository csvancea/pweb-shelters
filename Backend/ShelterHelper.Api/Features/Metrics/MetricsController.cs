using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter;
using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelterHelper.Api.Features.Metrics
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IViewMetricsAboutSingleShelterQueryHandler viewMetricsAboutSingleShelterQueryHandler;
        private readonly IViewMetricsAboutAllSheltersQueryHandler viewMetricsAboutAllSheltersQueryHandler;

        public MetricsController(IViewMetricsAboutSingleShelterQueryHandler viewMetricsAboutSingleShelterQueryHandler, IViewMetricsAboutAllSheltersQueryHandler viewMetricsAboutAllSheltersQueryHandler)
        {
            this.viewMetricsAboutSingleShelterQueryHandler = viewMetricsAboutSingleShelterQueryHandler;
            this.viewMetricsAboutAllSheltersQueryHandler = viewMetricsAboutAllSheltersQueryHandler;
        }

        [HttpGet("metricsAboutShelter/{id}")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ViewMetricsAboutShelterDto>> ViewMetricsAboutShelterAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var metric = await viewMetricsAboutSingleShelterQueryHandler.HandleAsync(id, cancellationToken);

            return metric;
        }

        [HttpGet("metricsAboutAllShelters")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ViewMetricsAboutAllSheltersDto>> ViewMetricsAboutAllSheltersAsync(CancellationToken cancellationToken)
        {
            var metric = await viewMetricsAboutAllSheltersQueryHandler.HandleAsync(cancellationToken);

            return metric;
        }
    }
}
