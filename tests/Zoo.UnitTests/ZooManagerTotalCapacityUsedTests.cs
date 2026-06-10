using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerTotalCapacityUsedTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    public void TC007_TotalCapacityUsed_CountsCriticalAnimalAsTwoPlaces()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Create(1, status: HealthStatus.Healthy));
        zoo.AddAnimal(TestAnimals.Create(2, status: HealthStatus.Critical));

        // Act
        var result = zoo.TotalCapacityUsed;

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-007")]
    public void TC017_TotalCapacityUsed_ReturnsZeroForEmptyZoo()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.TotalCapacityUsed;

        // Assert
        result.Should().Be(0);
    }
}
