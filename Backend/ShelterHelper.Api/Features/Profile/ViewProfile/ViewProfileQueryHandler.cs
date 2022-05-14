using ShelterHelper.Api.Web;
using ShelterHelper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Features.Profile.ViewProfile
{
    public class ViewProfileQueryHandler : IViewProfileQueryHandler
    {
        private readonly ShelterHelperContext dbContext;

        public ViewProfileQueryHandler(ShelterHelperContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProfileDto> HandleAsync(string identityId, CancellationToken cancellationToken)
        {
            var userProfile = await dbContext.Users
                .Where(x => x.IdentityId == identityId)
                .Select(x => new ProfileDto(x.Email, x.Name, x.PhoneNumber, x.Address, x.BirthDate))
                .FirstOrDefaultAsync(cancellationToken);

            if (userProfile == null)
            {
                throw new ApiException(System.Net.HttpStatusCode.Unauthorized, "This user does not have a registered profile!");
            }

            return userProfile;
        }
    }
}
