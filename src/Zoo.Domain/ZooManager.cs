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

    private static readonly Dictionary<AnimalCategory, double> BaseRations = new()
    {
        [AnimalCategory.Carnivore] = 5.0,
        [AnimalCategory.Herbivore] = 2.0,
        [AnimalCategory.Omnivore]  = 3.0,
    };

    private static readonly Dictionary<AnimalCategory, double> BaseCosts = new()
    {
        [AnimalCategory.Carnivore] = 25.0,
        [AnimalCategory.Herbivore] =  8.0,
        [AnimalCategory.Omnivore]  = 15.0,
    };

    private const double SickRationMultiplier = 0.70;

    public double CalculateDailyRation(int animalId)
    {
        var animal = _animals[animalId];
        double baseRation = BaseRations[animal.Category];
        return animal.Status == HealthStatus.Sick ? baseRation * SickRationMultiplier : baseRation;
    }

    public double CalculateDailyCost()
        => _animals.Values.Sum(a => BaseCosts[a.Category]);

    public IReadOnlyList<Animal> GetCriticalAnimals() => throw new NotImplementedException();
    public bool RemoveAnimal(int id) => throw new NotImplementedException();
}
