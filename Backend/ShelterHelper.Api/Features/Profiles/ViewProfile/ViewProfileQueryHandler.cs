using ShelterHelper.Api.Web;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Profiles.ViewProfile
{
    public class ViewProfileQueryHandler : IViewProfileQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewProfileQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProfileAndCurrentShelterBookingDto> HandleAsync(string identityId, CancellationToken cancellationToken)
        {
            var userProfile = await dbContext.Users.FirstOrDefaultAsync(x => x.IdentityId == identityId);

            if (userProfile == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.Unauthorized, $"User with identityId {identityId} does not have a registered profile!");
            }

            return await HandleAsyncImpl(userProfile.Id, cancellationToken);
        }

        public async Task<ProfileAndCurrentShelterBookingDto> HandleAsync(int userId, CancellationToken cancellationToken)
        {
            var userProfile = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (userProfile == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.Unauthorized, $"User with userId {userId} does not have a registered profile!");
            }

            return await HandleAsyncImpl(userId, cancellationToken);
        }

        private async Task<ProfileAndCurrentShelterBookingDto> HandleAsyncImpl(int userId, CancellationToken cancellationToken)
        {
            var query = from user in dbContext.Users
                        join booking in dbContext.Bookings on new { UserId = user.Id, ActualCheckOutDate = (DateTime?)null } equals new { booking.UserId, booking.ActualCheckOutDate } into bj
                        from booking in bj.DefaultIfEmpty()
                        join shelter in dbContext.Shelters on booking.ShelterId equals shelter.Id into sj
                        from shelter in sj.DefaultIfEmpty()
                        where user.Id == userId
                        select new ProfileAndCurrentShelterBookingDto(user, shelter, booking);

            var result = await query
                .AsNoTracking()
                .FirstAsync(cancellationToken);

            return result;
        }
    }
}
