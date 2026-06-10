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
        var animal = TestAnimals.Simba();

        // Act
        var result = zoo.AddAnimal(animal);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-005")]
    public void TC005_AddAnimal_ThrowsDuplicateAnimalExceptionForExistingId()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Simba());

        // Act
        Action act = () => zoo.AddAnimal(TestAnimals.SimbaSick());

        // Assert
        act.Should().Throw<DuplicateAnimalException>()
            .WithMessage("*1 already exists*");
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-006")]
    public void TC006_AddAnimal_ThrowsZooCapacityExceededExceptionWhenFull()
    {
        // Arrange
        var zoo = new ZooManager();
        for (var id = 1; id <= ZooManager.MaxCapacity; id++)
            zoo.AddAnimal(TestAnimals.Create(id));

        // Act
        Action act = () => zoo.AddAnimal(TestAnimals.Create(ZooManager.MaxCapacity + 1));

        // Assert
        act.Should().Throw<ZooCapacityExceededException>();
    }
}
