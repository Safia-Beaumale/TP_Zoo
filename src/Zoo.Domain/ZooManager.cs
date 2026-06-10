namespace Zoo.Domain;

public class ZooManager
{
    public const int MaxCapacity = 50;

    private readonly Dictionary<int, Animal> _animals = new();

    public int AddAnimal(Animal animal)
    {
        if (_animals.ContainsKey(animal.Id))
            throw new DuplicateAnimalException(animal.Id);

        if (TotalCapacityUsed + PlacesRequiredFor(animal) > MaxCapacity)
            throw new ZooCapacityExceededException();

        _animals[animal.Id] = animal;
        return animal.Id;
    }

    public Animal? GetAnimal(int id) => _animals.GetValueOrDefault(id);

    public int TotalAnimals => _animals.Count;

    public int TotalCapacityUsed => _animals.Values.Sum(PlacesRequiredFor);

    private static int PlacesRequiredFor(Animal animal) =>
        animal.Status == HealthStatus.Critical ? 2 : 1;

    public double CalculateDailyRation(int animalId)
    {
        var animal = _animals[animalId];
        double baseRation;
        if (animal.Category == AnimalCategory.Carnivore) baseRation = 5.0;
        else if (animal.Category == AnimalCategory.Herbivore) baseRation = 2.0;
        else baseRation = 3.0;
        if (animal.Status == HealthStatus.Sick) return baseRation * 0.70;
        return baseRation;
    }

    public double CalculateDailyCost()
    {
        double total = 0;
        foreach (var animal in _animals.Values)
        {
            double cost;
            if (animal.Category == AnimalCategory.Carnivore) cost = 25.0;
            else if (animal.Category == AnimalCategory.Herbivore) cost = 8.0;
            else cost = 15.0;
            total += cost;
        }
        return total;
    }

    public IReadOnlyList<Animal> GetCriticalAnimals() => throw new NotImplementedException();
    public bool RemoveAnimal(int id) => throw new NotImplementedException();
}
