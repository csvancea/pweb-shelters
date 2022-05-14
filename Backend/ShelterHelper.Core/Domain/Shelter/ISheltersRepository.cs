using ShelterHelper.Core.DataModel;
using ShelterHelper.Core.SeedWork;

namespace ShelterHelper.Core.Domain.Shelter
{
    public interface ISheltersRepository : IRepositoryOfAggregate<Shelters, RegisterShelterCommand>
    {
        public Task DeleteShelterAsync(int shelterId, CancellationToken cancellationToken);
    }
}
