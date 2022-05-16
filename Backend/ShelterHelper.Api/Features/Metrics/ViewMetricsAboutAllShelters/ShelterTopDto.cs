namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutAllShelters
{
    public record ShelterTopDto
    {
        public ShelterTopDto(int id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }
        public int Id { get; init; }
        public string Name { get; init; }

        public int Count { get; init; }
    }
}
