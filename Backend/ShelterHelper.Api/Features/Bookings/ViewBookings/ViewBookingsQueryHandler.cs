using ShelterHelper.Api.Web;
using ShelterHelper.Core.Domain.UserBookings;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ShelterHelper.Api.Features.Bookings.ViewBookings
{
    public class ViewBookingsQueryHandler : IViewBookingsQueryHandler
    {
        private readonly ShelterHelperContext dbContext;
        
        public ViewBookingsQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<RentalHistoryDto>> HandleAsync(string identityId, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.IdentityId == identityId);

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            return await HandleAsyncImpl(user.Id, cancellationToken);
        }

        public async Task<IEnumerable<RentalHistoryDto>> HandleAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with ID {userId} does not exist");
            }

            return await HandleAsyncImpl(userId, cancellationToken);
        }

        private async Task<IEnumerable<RentalHistoryDto>> HandleAsyncImpl(int userId, CancellationToken cancellationToken)
        {
            var query = from booking in dbContext.Bookings
                        where booking.UserId == userId
                        join book in dbContext.Shelters
                            on booking.ShelterId equals book.Id into grouping
                        from book in grouping.DefaultIfEmpty()
                        orderby booking.Id descending
                        select new RentalHistoryDto(book, booking);

            var result = await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
