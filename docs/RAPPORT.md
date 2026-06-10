# TP Zoo — Rapport de développement TDD

## Contexte

Développement d'une bibliothèque de gestion de zoo (`Zoo.Domain`) en suivant la méthodologie **TDD** (Red → Green → Refacto), avec 19 cas de tests couvrant 15 exigences fonctionnelles.

---

## Architecture du projet

```
Zoo.sln
├── src/Zoo.Domain/         # Logique métier
│   ├── Animal.cs           # Entité principale
│   ├── AnimalCategory.cs   # Enum : Carnivore, Herbivore, Omnivore
│   ├── HealthStatus.cs     # Enum : Healthy, Sick, Critical
│   ├── ZooManager.cs       # Classe centrale (toute la logique)
│   └── Exceptions.cs       # DuplicateAnimalException, ZooCapacityExceededException
└── tests/Zoo.UnitTests/    # Tests xUnit + FluentAssertions
```

---

## Fonctionnalités implémentées

### Gestion des animaux
| Méthode | Description |
|---|---|
| `AddAnimal(animal)` | Ajoute un animal, retourne son Id |
| `GetAnimal(id)` | Retourne l'animal ou `null` |
| `RemoveAnimal(id)` | Supprime un animal, retourne `true/false` |
| `TotalAnimals` | Nombre d'animaux dans le zoo |

### Capacité
| Règle | Détail |
|---|---|
| Capacité max | 50 places |
| Animal `Critical` | Occupe **2 places** (nécessite surveillance renforcée) |
| `TotalCapacityUsed` | Somme des places occupées (avec règle Critical) |

### Calculs journaliers
| Méthode | Logique |
|---|---|
| `CalculateDailyRation(id)` | Ration de base par catégorie × 0.70 si `Sick` |
| `CalculateDailyCost()` | Coût de base par catégorie + surcoût santé |

**Rations de base (kg/jour) :** Carnivore = 5.0 · Herbivore = 2.0 · Omnivore = 3.0

**Coûts de base (€/jour) :** Carnivore = 25 · Herbivore = 8 · Omnivore = 15

**Surcoûts santé (€/jour) :** Healthy = +0 · Sick = +20 · Critical = +50

### Alertes
| Méthode | Description |
|---|---|
| `GetCriticalAnimals()` | Retourne la liste des animaux en état `Critical` |

---

## Cas de tests (19 au total)

| TC | Classe | Ce que ça vérifie |
|---|---|---|
| TC001 | Add | `AddAnimal` retourne l'Id assigné |
| TC002 | Get | `GetAnimal` retourne l'animal correspondant |
| TC003 | Get | `GetAnimal` retourne `null` pour un Id inconnu |
| TC004 | Total | `TotalAnimals` reflète le nombre d'animaux ajoutés |
| TC005 | Add | Lève `DuplicateAnimalException` si l'Id existe déjà |
| TC006 | Add | Lève `ZooCapacityExceededException` quand le zoo est plein |
| TC007 | Capacity | Un animal `Critical` compte pour 2 places |
| TC008 | Ration | Carnivore sain → 5.0 kg/jour |
| TC009 | Ration | Carnivore malade → 3.5 kg/jour (×0.70) |
| TC010 | Cost | Carnivore sain + Herbivore sain → 33 €/jour |
| TC011 | Cost | Carnivore malade → 45 €/jour (25 + 20) |
| TC012 | Cost | Carnivore critique → 75 €/jour (25 + 50) |
| TC013 | Critical | `GetCriticalAnimals` retourne uniquement les `Critical` |
| TC014 | Remove | `RemoveAnimal` supprime l'animal et retourne `true` |
| TC015 | Remove | `RemoveAnimal` retourne `false` pour un Id inconnu |
| TC016 | Total | `TotalAnimals` = 0 pour un zoo vide |
| TC017 | Capacity | `TotalCapacityUsed` = 0 pour un zoo vide |
| TC018 | Critical | `GetCriticalAnimals` retourne liste vide si aucun critique |
| TC019 | Add | Lève `ZooCapacityExceededException` si `Critical` ne rentre pas (1 place restante) |

---

## Déroulement TDD

Chaque groupe de tests a suivi les 3 phases :

| Phase | Commits couverts |
|---|---|
| 🔴 **Red** — tests écrits, code absent → échec attendu | TC001-003, TC004-007, TC008-010, TC011-012, TC013-015 |
| 🟢 **Green** — implémentation minimale pour faire passer | TC001-003, TC004-007, TC008-010, TC011-012, TC013-015 |
| 🔵 **Refacto** — nettoyage sans casser les tests | TC001-003, TC004-007, TC008-010, TC011-012, TC013-015 |
| ✅ **Directs** (Green+Refacto) | TC016-019 |

Un correctif (`[FIX]`) a été appliqué sur TC005 : le doublon utilisé était `Nala` (Id=2) au lieu de `SimbaSick` (Id=1), rendant le test incohérent.

---

## Résultat

- **19 tests** — tous passants ✅
- **0 dépendance externe** (pas de BDD, pas de mock)
- Code métier concentré dans `ZooManager.cs` (~67 lignes)
