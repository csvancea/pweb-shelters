using ShelterHelper.Api.Features.Profile.RegisterProfile;
using ShelterHelper.Api.Features.Profile.ViewProfile;

namespace ShelterHelper.Api.Features.Profile
{
    internal static class ProfilesModule
    {
        internal static void AddProfilesHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRegisterProfileCommandHandler, RegisterProfileCommandHandler>();
            services.AddTransient<IViewProfileQueryHandler, ViewProfileQueryHandler>();
        }
    }
}
