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

        public async Task<ViewMetricsAboutBookDto> HandleAsync(int shelterId, CancellationToken cancellationToken)
        {
            var query = from shelter in dbContext.Shelters
                        where shelter.Id == shelterId
                        join booking in dbContext.Bookings.Include(x => x.Reader)
                            on shelter.Id equals booking.ShelterId into grouping
                        from booking in grouping.DefaultIfEmpty()
                        select new BookRentalQueryDto(shelter, booking);

            var result = await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var metric = new ViewMetricsAboutBookDto(shelterId, result[0].BookName, result[0].AuthorName);

            foreach (var bookRental in result)
            {
                var rentalDto = bookRental.ValidateAndParseRental();

                if (rentalDto != null)
                {
                    metric.AddRentalToHistory(rentalDto);
                }
            }

            metric.DivideAverages();

            return metric;
        }
    }
}
