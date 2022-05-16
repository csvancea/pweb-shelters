using ShelterHelper.Api.Features.Shelter;
using ShelterHelper.Api.Features.Metrics;
using ShelterHelper.Api.Features.Profiles;
using ShelterHelper.Api.Features.Bookings;

namespace ShelterHelper.Api.Web
{
    public static class ApiFeaturesExtensions
    {
        public static void AddApiFeaturesHandlers(this IServiceCollection services)
        {
            // Add Shelter Handlers
            services.AddSheltersHandlers();

            // Add Profile Handlers
            services.AddProfilesHandlers();

            // Add Booking Handlers
            services.AddBookingsHandlers();

            // Add Metrics Handlers
            services.AddMetricsHandlers();
        }
    }
}
