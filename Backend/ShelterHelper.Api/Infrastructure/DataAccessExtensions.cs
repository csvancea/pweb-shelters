using ShelterHelper.Core.Domain.UserBookings;
using ShelterHelper.Core.Domain.Shelter;
using ShelterHelper.Infrastructure.Data;
using ShelterHelper.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Api.Infrastructure
{
    public static partial class DataAccessExtensions
    {
        public static void AddShelterHelperDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ShelterHelperContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("ShelterHelperDb")));
        }

        public static void AddShelterHelperAggregateRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISheltersRepository, SheltersRepository>();
            services.AddTransient<IUsersBookingsRepository, UsersBookingsRepository>();
        }
    }
}
