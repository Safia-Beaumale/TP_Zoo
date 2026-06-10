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

    [Fact]
    [Trait("Requirement", "REQ-Z-011")]
    public void TC011_CalculateDailyCost_CarnivoreSick_Returns45()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.SimbaSick());

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(45.0);
    }

    [Fact]
    [Trait("Requirement", "REQ-Z-012")]
    public void TC012_CalculateDailyCost_CarnivoreCritical_Returns75()
    {
        // Arrange
        var zoo = new ZooManager();
        zoo.AddAnimal(TestAnimals.SimbaCritical());

        // Act
        var result = zoo.CalculateDailyCost();

        // Assert
        result.Should().Be(75.0);
    }
}
