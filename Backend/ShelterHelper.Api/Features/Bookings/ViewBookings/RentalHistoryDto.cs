namespace ShelterHelper.Api.Features.Bookings.ViewBookings
{
    public record RentalHistoryDto
    {
        public RentalHistoryDto(Core.DataModel.Shelters shelter, Core.DataModel.Bookings booking)
        {
            ShelterId = shelter.Id;
            ShelterName = shelter.Name;
            checkInDate = booking.CheckInDate;
            checkOutDate = booking.ActualCheckOutDate;
        }

        public int ShelterId { get; init; }
        public string ShelterName { get; init; }
        public DateTime checkInDate { get; init; }
        public DateTime? checkOutDate { get; init; }
    }
}
