using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Infrastructure.Data.Repositories
{
    public class SheltersRepository : ISheltersRepository
    {
        private readonly ShelterHelperContext context;

        public SheltersRepository(ShelterHelperContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(RegisterShelterCommand command, CancellationToken cancellationToken)
        {
            var shelter = new Shelters(command.Name, command.Address, command.MapsLink, command.Capacity, command.MaximumDaysForRental, command.NumberOfUsers, command.Accessibility, command.Pets);

            await context.Shelters.AddAsync(shelter, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task<DomainOfAggregate<Shelters>?> GetAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var shelter = await context.Shelters
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (shelter == null)
            {
                return null;
            }

            return new SheltersDomain(shelter);
        }

        public async Task DeleteShelterAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var shelter = await context.Shelters
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (shelter != null)
            {
                context.Shelters.Remove(shelter);
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
