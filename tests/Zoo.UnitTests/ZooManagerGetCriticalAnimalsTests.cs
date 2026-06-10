using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerGetCriticalAnimalsTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-013")]
    public void TC013_GetCriticalAnimals_ReturnsOnlyCriticalAnimals()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Create(1, status: HealthStatus.Healthy));
        zoo.AddAnimal(TestAnimals.Create(2, status: HealthStatus.Sick));
        zoo.AddAnimal(TestAnimals.Create(3, status: HealthStatus.Critical));
        zoo.AddAnimal(TestAnimals.Create(4, status: HealthStatus.Critical));

        // Act
        var result = zoo.GetCriticalAnimals();

        // Assert
        result.Should().HaveCount(2);
        result.Should().OnlyContain(animal => animal.Status == HealthStatus.Critical);
    }
}
