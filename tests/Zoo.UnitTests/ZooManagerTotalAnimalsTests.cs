using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerTotalAnimalsTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-004")]
    public void TC004_TotalAnimals_ReflectsNumberOfAddedAnimals()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Create(1));
        zoo.AddAnimal(TestAnimals.Create(2));
        zoo.AddAnimal(TestAnimals.Create(3));

        // Act
        var result = zoo.TotalAnimals;

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-004")]
    public void TC016_TotalAnimals_ReturnsZeroForEmptyZoo()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.TotalAnimals;

        // Assert
        result.Should().Be(0);
    }
}
