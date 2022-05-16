using ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShelterHelper.Api.Features.Metrics
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IViewMetricsAboutSingleShelterQueryHandler viewMetricsAboutSingleShelterQueryHandler;

        public MetricsController(IViewMetricsAboutSingleShelterQueryHandler viewMetricsAboutSingleShelterQueryHandler)
        {
            this.viewMetricsAboutSingleShelterQueryHandler = viewMetricsAboutSingleShelterQueryHandler;
        }

        [HttpGet("metricsAboutShelter/{id}")]
        [Authorize("AdminAccess")]
        public async Task<ActionResult<ViewMetricsAboutShelterDto>> ViewMetricsAboutShelterAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var metric = await viewMetricsAboutSingleShelterQueryHandler.HandleAsync(id, cancellationToken);

            return metric;
        }
    }
}
