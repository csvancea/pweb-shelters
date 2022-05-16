using ShelterHelper.Api.Web;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public class ViewMetricsAboutSingleShelterQueryHandler : IViewMetricsAboutSingleShelterQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewMetricsAboutSingleShelterQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ViewMetricsAboutShelterDto> HandleAsync(int shelterId, CancellationToken cancellationToken)
        {
            var query = from shelter in dbContext.Shelters
                        where shelter.Id == shelterId
                        join booking in dbContext.Bookings.Include(x => x.User)
                            on shelter.Id equals booking.ShelterId into grouping
                        from booking in grouping.DefaultIfEmpty()
                        orderby booking.Id descending
                        select new ShelterBookingQueryDto(shelter, booking);

            var result = await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var metric = new ViewMetricsAboutShelterDto(shelterId, result[0].ShelterName, result[0].ShelterTotalNumberOfRefugees);

            foreach (var shelterBooking in result)
            {
                var refugeeDto = shelterBooking.ValidateAndParseRefugee();

                if (refugeeDto != null)
                {
                    metric.AddRefugeeToHistory(refugeeDto);
                }
            }

            metric.DivideAverages();

            return metric;
        }
    }
}
