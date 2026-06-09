# Exigences et Cas de Test — ZooManager

## Contexte

Implémentation du cœur métier du Zoo Municipal de Lyon en suivant la méthode TDD.
Classe cible: `ZooManager`

---

## Cas de test associés

### REQ-Z-001 — AddAnimal retourne l'ID assigné

- **Arrange** : créer un `ZooManager` vide, instancier un animal avec id=1.
- **Act** : appeler `AddAnimal(animal)`.
- **Assert** : la valeur retournée est égale à 1.

---

### REQ-Z-002 — GetAnimal retourne l'animal existant

- **Arrange** : ajouter un animal avec id=1.
- **Act** : appeler `GetAnimal(1)`.
- **Assert** : l'objet retourné correspond à l'animal ajouté.

---

### REQ-Z-003 — GetAnimal retourne null pour un ID inexistant

- **Arrange** : `ZooManager` vide.
- **Act** : appeler `GetAnimal(999)`.
- **Assert** : résultat est `null`.

---

### REQ-Z-004 — TotalAnimals reflète le nombre d'animaux ajoutés

- **Arrange** : ajouter 3 animaux.
- **Act** : appeler `TotalAnimals()`.
- **Assert** : résultat est 3.

---

### REQ-Z-005 — AddAnimal lève DuplicateAnimalException si l'ID existe déjà

- **Arrange** : ajouter un animal avec id=1.
- **Act** : tenter d'ajouter un second animal avec id=1.
- **Assert** : `DuplicateAnimalException` est levée.

---

### REQ-Z-006 — AddAnimal refuse l'admission si la capacité (50) est atteinte

- **Arrange** : remplir le zoo avec 50 animaux Healthy.
- **Act** : tenter d'ajouter un 51ᵉ animal.
- **Assert** : une exception de capacité dépassée est levée.

---

### REQ-Z-007 — Un animal Critical occupe 2 places dans TotalCapacityUsed

- **Arrange** : ajouter 1 animal Healthy et 1 animal Critical.
- **Act** : appeler `TotalCapacityUsed()`.
- **Assert** : résultat est 3 (1 + 2).

---

### REQ-Z-008 — CalculateDailyRation d'un carnivore Healthy = 5 kg

- **Arrange** : animal Carnivore, statut Healthy.
- **Act** : appeler `CalculateDailyRation(animal)`.
- **Assert** : résultat est 5.0 kg.

---

### REQ-Z-009 — CalculateDailyRation d'un animal Sick = ration de base − 30 %

- **Arrange** : animal Carnivore (base 5 kg), statut Sick.
- **Act** : appeler `CalculateDailyRation(animal)`.
- **Assert** : résultat est 3.5 kg (5 × 0.70).

---

### REQ-Z-010 — CalculateDailyCost retourne le coût total correct

- **Arrange** : 1 carnivore Healthy (25 €) + 1 herbivore Healthy (8 €).
- **Act** : appeler `CalculateDailyCost()`.
- **Assert** : résultat est 33 €.

---

### REQ-Z-011 — Un animal Sick ajoute +20 € au coût quotidien

- **Arrange** : 1 carnivore Sick (25 € + 20 €).
- **Act** : appeler `CalculateDailyCost()`.
- **Assert** : résultat est 45 €.

---

### REQ-Z-012 — Un animal Critical ajoute +50 € au coût quotidien

- **Arrange** : 1 carnivore Critical (25 € + 50 €).
- **Act** : appeler `CalculateDailyCost()`.
- **Assert** : résultat est 75 €.

---

### REQ-Z-013 — GetCriticalAnimals retourne uniquement les animaux Critical

- **Arrange** : ajouter 1 animal Healthy, 1 Sick, 2 Critical.
- **Act** : appeler `GetCriticalAnimals()`.
- **Assert** : la liste contient exactement les 2 animaux Critical.

---

### REQ-Z-014 — RemoveAnimal retire l'animal et retourne true

- **Arrange** : ajouter un animal avec id=1.
- **Act** : appeler `RemoveAnimal(1)`.
- **Assert** : retourne `true` ; `GetAnimal(1)` retourne ensuite `null`.

---

### REQ-Z-015 — RemoveAnimal retourne false pour un ID inexistant

- **Arrange** : `ZooManager` vide.
- **Act** : appeler `RemoveAnimal(999)`.
- **Assert** : retourne `false`.
