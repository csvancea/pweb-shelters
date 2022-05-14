namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public record ViewMetricsAboutBookDto
    {
        public ViewMetricsAboutBookDto(int id, string name, string authorName)
        {
            Id = id;
            Name = name;
            AuthorName = authorName;
            AverageRentalTime = 0;
            AverageRentalCompletion = 0;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public string AuthorName { get; init; }
        public double AverageRentalTime { get; private set; }
        public double AverageRentalCompletion { get; private set; }
        public List<RentalsDto> RentalHistory { get; } = new();
        
        internal void AddRentalToHistory(RentalsDto rental)
        {
            RentalHistory.Add(rental);

            AverageRentalTime += rental.RentalEndDate.HasValue ? (rental.RentalEndDate.Value - rental.RentalStartDate).Days : (DateTime.Now - rental.RentalStartDate).Days;
            AverageRentalCompletion += rental.CompletedInTime ? 1 : 0;
        }

        internal void DivideAverages()
        {
            int rentalHistoryCount = RentalHistory.Any() ? RentalHistory.Count() : 1;

            AverageRentalTime /= rentalHistoryCount;
            AverageRentalCompletion /= rentalHistoryCount;
        }
    }
}
