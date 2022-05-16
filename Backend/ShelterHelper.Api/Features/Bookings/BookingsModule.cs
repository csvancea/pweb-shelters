using ShelterHelper.Api.Features.Bookings.ShelterCheckIn;
using ShelterHelper.Api.Features.Bookings.ShelterCheckInExtend;
using ShelterHelper.Api.Features.Bookings.ShelterCheckOut;
using ShelterHelper.Api.Features.Bookings.ViewBookings;

namespace ShelterHelper.Api.Features.Bookings
{
    internal static class BookingsModule
    {
        internal static void AddBookingsHandlers(this IServiceCollection services)
        {
            services.AddTransient<IShelterCheckInCommandHandler, ShelterCheckInCommandHandler>();
            services.AddTransient<IShelterCheckInExtendCommandHandler, ShelterCheckInExtendCommandHandler>();
            services.AddTransient<IShelterCheckOutCommandHandler, ShelterCheckOutCommandHandler>();
            services.AddTransient<IViewBookingsQueryHandler, ViewBookingsQueryHandler>();
        }
    }
}
