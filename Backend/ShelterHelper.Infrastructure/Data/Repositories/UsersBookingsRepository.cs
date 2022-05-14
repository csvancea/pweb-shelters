using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.Domain;
using ShelterHelper.Core.Domain.UserBookings;
using ShelterHelper.Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Infrastructure.Data.Repositories
{
    public class UsersBookingsRepository : IUsersBookingsRepository
    {
        private readonly ShelterHelperContext context;

        public UsersBookingsRepository(ShelterHelperContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(RegisterUserProfileCommand command, CancellationToken cancellationToken)
        {
            var user = new Users(command.IdentityId, command.Email, command.Name, command.PhoneNumber, command.Address, command.BirthDate);

            await context.Users.AddAsync(user);
            await SaveAsync(cancellationToken);
        }

        public async Task<DomainOfAggregate<Users>?> GetAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersBookingsDomain(entity);
        }

        public async Task<DomainOfAggregate<Users>?> GetByIdentityAsync(string identityId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.IdentityId == identityId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersBookingsDomain(entity);
        }

        public async Task<DomainOfAggregate<Users>?> GetWithoutRentalsAsync(int aggregateId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .FirstOrDefaultAsync(x => x.Id == aggregateId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersBookingsDomain(entity);
        }

        public async Task<DomainOfAggregate<Users>?> GetByIdentityWithoutRentalsAsync(string identityId, CancellationToken cancellationToken)
        {
            var entity = await context.Users
                .FirstOrDefaultAsync(x => x.IdentityId == identityId, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return new UsersBookingsDomain(entity);
        }

        public Task SaveAsync(CancellationToken cancellationToken) => context.SaveChangesAsync(cancellationToken);
    }
}
