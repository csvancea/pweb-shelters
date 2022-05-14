namespace ShelterHelper.Api.Features.Bookings.ViewBookings
{
    public interface IViewBookingsQueryHandler
    {
        public Task<IEnumerable<RentalHistoryDto>> HandleAsync(string identityId, CancellationToken cancellationToken);
    }
}
