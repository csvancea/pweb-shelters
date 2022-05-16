namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public record AgeDistributionDto
    {
        public AgeDistributionDto(int age, int count)
        {
            Age = age;
            Count = count;
        }
        public int Age { get; init; }

        public int Count { get; init; }
    }
}
