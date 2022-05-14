namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public record RentalsDto
    {
        private readonly DateTime expectedRentalEndDate;

        public RentalsDto(string email, string phoneNumber, DateTime expectedRentalEndDate, DateTime rentalStartDate, DateTime? rentalEndDate)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            this.expectedRentalEndDate = expectedRentalEndDate;
            RentalStartDate = rentalStartDate;
            RentalEndDate = rentalEndDate;
        }

        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public bool CompletedInTime { get => RentalEndDate.HasValue && expectedRentalEndDate >= RentalEndDate.Value; }
        public DateTime RentalStartDate { get; init; }
        public DateTime? RentalEndDate { get; init; }
    }
}
