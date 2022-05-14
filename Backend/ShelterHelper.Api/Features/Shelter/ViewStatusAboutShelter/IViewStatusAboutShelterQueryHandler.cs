namespace ShelterHelper.Api.Features.Shelter.ViewStatusAboutShelter
{
    public interface IViewStatusAboutShelterQueryHandler
    {
        public Task<ShelterWithBookingHistoryDto> HandleAsync(int bookId, CancellationToken cancellationToken);
    }
}
