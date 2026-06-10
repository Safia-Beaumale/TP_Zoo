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
}
