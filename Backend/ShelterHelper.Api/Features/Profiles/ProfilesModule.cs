using ShelterHelper.Api.Features.Profiles.RegisterProfile;
using ShelterHelper.Api.Features.Profiles.UpdateProfile;
using ShelterHelper.Api.Features.Profiles.ViewProfile;
using ShelterHelper.Api.Features.Profiles.ViewAllProfiles;
using ShelterHelper.Api.Features.Profiles.DeleteProfile;

namespace ShelterHelper.Api.Features.Profiles
{
    internal static class ProfilesModule
    {
        internal static void AddProfilesHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRegisterProfileCommandHandler, RegisterProfileCommandHandler>();
            services.AddTransient<IUpdateProfileCommandHandler, UpdateProfileCommandHandler>();
            services.AddTransient<IViewProfileQueryHandler, ViewProfileQueryHandler>();
            services.AddTransient<IViewAllProfilesQueryHandler, ViewAllProfilesQueryHandler>();
            services.AddTransient<IDeleteProfileHandler, DeleteProfileHandler>();
        }
    }
}
