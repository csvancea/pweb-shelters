namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public record ViewMetricsAboutAllSheltersDto
    {
        public ViewMetricsAboutAllSheltersDto(IEnumerable<RefugeeCountDto> refugeeCounts, IEnumerable<AgeDistributionDto> ageDistribution, IEnumerable<ShelterTopDto> topShelters)
        {
            RefugeeCounts = refugeeCounts;
            AgeDistribution = ageDistribution;
            TopShelters = topShelters;
        }

        public IEnumerable<RefugeeCountDto> RefugeeCounts { get; init; }
        public IEnumerable<AgeDistributionDto> AgeDistribution { get; init; }
        public IEnumerable<ShelterTopDto> TopShelters { get; init; }
    }
}
