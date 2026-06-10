using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerDailyCostTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-010")]
    public void TC010_CalculateDailyCost_OneCarnivoreOneHerbivoreHealthy_Returns33()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Simba());
        zoo.AddAnimal(TestAnimals.Nala());

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(33.0);
    }
}
