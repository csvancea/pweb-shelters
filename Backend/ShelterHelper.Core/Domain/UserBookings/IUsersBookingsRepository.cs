using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.UserBookings
{
    public interface IUsersBookingsRepository : IRepositoryOfAggregate<Users, RegisterUserProfileCommand>
    {
        public Task<DomainOfAggregate<Users>?> GetByIdentityAsync (string identityId, CancellationToken cancellationToken);
        public Task<DomainOfAggregate<Users>?> GetWithoutRentalsAsync(int id, CancellationToken cancellationToken);
        public Task<DomainOfAggregate<Users>?> GetByIdentityWithoutRentalsAsync(string identityId, CancellationToken cancellationToken);

    }
}
