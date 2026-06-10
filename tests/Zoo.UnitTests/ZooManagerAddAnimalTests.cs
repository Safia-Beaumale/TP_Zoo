using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerAddAnimalTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-001")]
    public void TC001_AddAnimal_ReturnsAssignedId()
    {
        // Arrange
        var zoo = new ZooManager();
        var animal = new Animal
        {
            Id = 1,
            Name = "Simba",
            Category = AnimalCategory.Carnivore,
            Status = HealthStatus.Healthy
        };

        // Act
        var result = zoo.AddAnimal(animal);

        // Assert
        result.Should().Be(1);
    }
}
