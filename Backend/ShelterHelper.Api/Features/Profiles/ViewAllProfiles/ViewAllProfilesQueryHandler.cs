using ShelterHelper.Api.Features.Profiles.ViewProfile;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Profiles.ViewAllProfiles
{
    public class ViewAllProfilesQueryHandler : IViewAllProfilesQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewAllProfilesQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProfileAndCurrentShelterBookingDto>> HandleAsync(CancellationToken cancellationToken)
        {
            var query = from user in dbContext.Users
                        join booking in dbContext.Bookings on new { UserId = user.Id, ActualCheckOutDate = (DateTime?)null } equals new { booking.UserId, booking.ActualCheckOutDate } into bj
                        from booking in bj.DefaultIfEmpty()
                        join shelter in dbContext.Shelters on booking.ShelterId equals shelter.Id into sj
                        from shelter in sj.DefaultIfEmpty()
                        select new ProfileAndCurrentShelterBookingDto(user, shelter, booking);

            var result = await query
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
