using ShelterHelper.Api.Features.Shelter.AddShelter;
using ShelterHelper.Api.Features.Shelter.DeleteShelter;
using ShelterHelper.Api.Features.Shelter.ViewAllShelters;
using ShelterHelper.Api.Features.Shelter.ViewStatusAboutShelter;

namespace ShelterHelper.Api.Features.Shelter
{
    internal static class SheltersModule
    {
        internal static void AddBooksHandlers(this IServiceCollection services)
        {
            services.AddTransient<IAddShelterCommandHandler, AddShelterCommandHandler>();
            services.AddTransient<IViewAllSheltersQueryHandler, ViewAllSheltersQueryHandler>();
            services.AddTransient<IViewStatusAboutShelterQueryHandler, ViewStatusAboutShelterQueryHandler>();
            services.AddTransient<IDeleteShelterHandler, DeleteShelterHandler>();
        }
    }
}
