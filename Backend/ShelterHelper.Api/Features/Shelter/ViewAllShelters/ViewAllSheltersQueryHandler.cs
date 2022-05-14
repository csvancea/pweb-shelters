using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Shelter.ViewAllShelters
{
    public class ViewAllSheltersQueryHandler : IViewAllSheltersQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewAllSheltersQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ShelterDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var shelters = await dbContext.Shelters
                .AsNoTracking()
                .Select(x => new ShelterDto(x.Id, x.Name, x.Address, x.MapsLink, x.Capacity, x.MaximumDaysForRental, x.NumberOfUsers, x.Accessibility, x.Pets))
                .ToListAsync(cancellationToken);

            return shelters;
        }
    }
}
