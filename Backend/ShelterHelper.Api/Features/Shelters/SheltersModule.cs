using ShelterHelper.Api.Features.Shelters.AddShelter;
using ShelterHelper.Api.Features.Shelters.UpdateShelter;
using ShelterHelper.Api.Features.Shelters.DeleteShelter;
using ShelterHelper.Api.Features.Shelters.ViewAllShelters;
using ShelterHelper.Api.Features.Shelters.ViewStatusAboutShelter;

namespace ShelterHelper.Api.Features.Shelter
{
    internal static class SheltersModule
    {
        internal static void AddSheltersHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAddShelterCommandHandler, AddShelterCommandHandler>();
            services.AddTransient<IUpdateShelterCommandHandler, UpdateShelterCommandHandler>();
            services.AddTransient<IViewAllSheltersQueryHandler, ViewAllSheltersQueryHandler>();
            services.AddTransient<IViewStatusAboutShelterQueryHandler, ViewStatusAboutShelterQueryHandler>();
            services.AddTransient<IDeleteShelterHandler, DeleteShelterHandler>();
        }
    }
}
