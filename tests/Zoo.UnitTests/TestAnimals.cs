using Zoo.Domain;

namespace Zoo.UnitTests;

internal static class TestAnimals
{
    internal static Animal Simba() => new()
    {
        Id = 1,
        Name = "Simba",
        Category = AnimalCategory.Carnivore,
        Status = HealthStatus.Healthy
    };

    internal static Animal Nala() => new()
    {
        Id = 1,
        Name = "Nala",
        Category = AnimalCategory.Carnivore,
        Status = HealthStatus.Healthy
    };

    internal static Animal Create(
        int id,
        AnimalCategory category = AnimalCategory.Carnivore,
        HealthStatus status = HealthStatus.Healthy) => new()
    {
        Id = id,
        Name = $"Animal-{id}",
        Category = category,
        Status = status
    };
}
