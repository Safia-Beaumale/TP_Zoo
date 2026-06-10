using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerGetAnimalTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-002")]
    public void TC002_GetAnimal_ReturnsAnimalWithMatchingId()
    {
        // Arrange
        var zoo = new ZooManager();
        var animal = TestAnimals.Simba();
        zoo.AddAnimal(animal);

        // Act
        var result = zoo.GetAnimal(1);

        // Assert
        result.Should().BeEquivalentTo(animal);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-003")]
    public void TC003_GetAnimal_ReturnsNullForUnknownId()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.GetAnimal(999);

        // Assert
        result.Should().BeNull();
    }
}
