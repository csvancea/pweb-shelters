namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public record RefugeeDto
    {
        // private readonly DateTime expectedRentalEndDate;

        public RefugeeDto(int id, string name, string email, string phoneNumber, DateTime? birthDate, DateTime checkInDate, DateTime checkOutDate, bool checkedOut)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            CheckedOut = checkedOut;
        }
        
        public int Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime? BirthDate { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public bool CheckedOut { get; init; }
    }
}
