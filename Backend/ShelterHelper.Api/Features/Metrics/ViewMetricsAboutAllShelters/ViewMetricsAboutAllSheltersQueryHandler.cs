using ShelterHelper.Api.Web;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public class ViewMetricsAboutAllSheltersQueryHandler : IViewMetricsAboutAllSheltersQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewMetricsAboutAllSheltersQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ViewMetricsAboutAllSheltersDto> HandleAsync(CancellationToken cancellationToken)
        {
            var refugeeCounts = new List<RefugeeCountDto>();
            for (var i = 30; i >= 0; i--)
            {
                var date = (DateTime.Now - TimeSpan.FromDays(i)).Date;
                var count = dbContext.Bookings.Count(b => b.CheckInDate.Date <= date && (!b.ActualCheckOutDate.HasValue || date <= b.ActualCheckOutDate.Value.Date));

                refugeeCounts.Add(new RefugeeCountDto(date, count));
            }

            var ageDistribution = await (
                    from user in dbContext.Users
                    where user.BirthDate.HasValue
                    join booking in dbContext.Bookings on new { UserId = user.Id, ActualCheckOutDate = (DateTime?)null } equals new { booking.UserId, booking.ActualCheckOutDate }
                    group user by (DateTime.Now.Year - user.BirthDate.Value.Year) into g
                    select new AgeDistributionDto(g.Key, g.Count())
                ).ToListAsync(cancellationToken);

            var topShelters = await dbContext.Shelters
                .OrderByDescending(s => s.NumberOfUsers)
                .Select(s => new ShelterTopDto(s.Id, s.Name, s.NumberOfUsers))
                .Take(15)
                .ToListAsync(cancellationToken);

            var metrics = new ViewMetricsAboutAllSheltersDto(refugeeCounts, ageDistribution, topShelters);

            return metrics;
        }
    }
}
