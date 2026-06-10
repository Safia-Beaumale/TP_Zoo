using FluentAssertions;
using Zoo.Domain;

namespace Zoo.UnitTests;

public class ZooManagerDailyRationTests
{
    [Fact]
    [Trait("Requirement", "REQ-Z-008")]
    public void TC008_CalculateDailyRation_CarnivoreHealthy_Returns5()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.Simba());

        // Act
        var result = zoo.CalculateDailyRation(1);

        // Assert
        result.Should().Be(5.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-009")]
    public void TC009_CalculateDailyRation_CarnivoreSick_Returns3Point5()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.SimbaSick());

        // Act
        var result = zoo.CalculateDailyRation(1);

        // Assert
        result.Should().Be(3.5);
    }
}
