namespace ShelterHelper.Api.Features.Metrics.ViewMetricsAboutShelter
{
    public record ViewMetricsAboutShelterDto
    {
        public ViewMetricsAboutShelterDto(int id, string name, int totalNumberOfRefugees)
        {
            Id = id;
            Name = name;
            TotalNumberOfRefugees = totalNumberOfRefugees;
            AverageRefugeeAge = 0;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public int TotalNumberOfRefugees { get; init; }
        public double AverageRefugeeAge { get; private set; }
        public List<RefugeeDto> RefugeeHistory { get; } = new();
        
        internal void AddRefugeeToHistory(RefugeeDto refugee)
        {
            RefugeeHistory.Add(refugee);

            if (refugee.BirthDate.HasValue)
            {
                AverageRefugeeAge += DateTime.Now.Year - refugee.BirthDate.Value.Year;
            }
        }

        internal void DivideAverages()
        {
            int averageCount = RefugeeHistory.Count(r => r.BirthDate.HasValue);
            if (averageCount == 0)
                averageCount = 1;

            AverageRefugeeAge /= averageCount;
        }
    }
}
