namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public record RefugeeCountDto
    {
        public RefugeeCountDto(DateTime date, int count)
        {
            Date = date;
            Count = count;
        }
        public DateTime Date { get; init; }

        public int Count { get; init; }
    }
}
