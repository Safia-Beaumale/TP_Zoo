using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerRemoveAnimalTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-014")]
    public void TC014_RemoveAnimal_RemovesAnimalAndReturnsTrue()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Simba());

        // Act
        var result = zoo.RemoveAnimal(1);

        // Assert
        result.Should().BeTrue();
        zoo.GetAnimal(1).Should().BeNull();
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-015")]
    public void TC015_RemoveAnimal_ReturnsFalseForUnknownId()
    {
        // Arrange
        var zoo = new ZooManager();

        // Act
        var result = zoo.RemoveAnimal(999);

        // Assert
        result.Should().BeFalse();
    }
}
