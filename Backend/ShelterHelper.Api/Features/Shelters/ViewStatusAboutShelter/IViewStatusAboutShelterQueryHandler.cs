namespace ShelterHelper.Api.Features.Shelters.ViewStatusAboutShelter
{
    public interface IViewStatusAboutShelterQueryHandler
    {
        public Task<ShelterWithBookingHistoryDto> HandleAsync(int bookId, CancellationToken cancellationToken);
    }
}
