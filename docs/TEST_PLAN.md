# Plan de test — ZooManager

## 1. Identification

| Champ    | Valeur                                          |
|----------|-------------------------------------------------|
| Projet   | Zoo - Système de gestion du Zoo Municipal de Lyon |
| Version  | 1.0                                             |
| Auteur   | Safia Beaumale-Mesmar, Kirill Rosier, Alexis Feron |
| Date     | 2026-06-10                                      |
| Statut   | Brouillon                                       |

---

## 2. Périmètre

### In scope

- Classe `ZooManager` (toutes les méthodes publiques)
- Classes `Animal`, `AnimalCategory`, `HealthStatus`
- Exceptions métier (`DuplicateAnimalException`, `ZooCapacityExceededException`)

### Out of scope

- Persistance en base de données
- Interface utilisateur
- API REST

---

## 3. Stratégie

- **Méthodologie** : Test-Driven Development (Red → Green → Refactor)
- **Framework** : xUnit + FluentAssertions
- **Couverture cible** : 100 % sur `ZooManager`

Chaque exigence est couverte par au moins un cas de test rédigé avant l'implémentation.  
Le cycle TDD est appliqué méthode par méthode : on écrit le test, on vérifie qu'il est rouge, puis on implémente le minimum de code pour le faire passer au vert, enfin on refactorise si nécessaire.

---

## 4. Critères d'entrée

- Spécifications validées (cf. `EXIGENCES.md`)
- Squelette de classes fourni (`ZooManager`, `Animal`, `AnimalCategory`, `HealthStatus`, exceptions)
- Projet de test `Zoo.UnitTests` configuré (xUnit, FluentAssertions, coverlet)

---

## 5. Critères de sortie

- 100 % des 15 exigences couvertes par au moins un test
- Tous les tests verts (0 échec, 0 ignoré)
- Couverture de lignes >= 95 % sur `ZooManager`

---

## 6. Environnement

| Composant         | Version |
|-------------------|---------|
| .NET              | 9.0     |
| xUnit             | 2.9.2   |
| FluentAssertions  | 6.12.0  |
| coverlet          | 6.0.2   |
| OS                | Windows 11 |

---

## 7. Cas de test prévus

---

### TC-001 — AddAnimal retourne l'ID de l'animal ajouté

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `ZooManager` vide ; `Animal { Id=1, Name="Simba", Category=Carnivore, Status=Healthy }` |
| **Attendu**      | `AddAnimal(animal)` retourne `1`                         |
| **Exigence**     | REQ-Z-001                                                |

---

### TC-002 — GetAnimal retourne l'animal correspondant à l'ID

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `Id=1` après ajout de l'animal `{ Id=1, Name="Simba" }`  |
| **Attendu**      | Objet retourné identique à l'animal ajouté               |
| **Exigence**     | REQ-Z-002                                                |

---

### TC-003 — GetAnimal retourne null pour un ID inexistant

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `ZooManager` vide ; `GetAnimal(999)`                     |
| **Attendu**      | `null`                                                   |
| **Exigence**     | REQ-Z-003                                                |

---

### TC-004 — TotalAnimals reflète le nombre d'animaux ajoutés

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 3 animaux ajoutés (Id=1, 2, 3)                           |
| **Attendu**      | `TotalAnimals` retourne `3`                              |
| **Exigence**     | REQ-Z-004                                                |

---

### TC-005 — AddAnimal lève DuplicateAnimalException si l'ID existe déjà

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | Animal `Id=1` déjà présent ; tentative d'ajout d'un second `Id=1` |
| **Attendu**      | `DuplicateAnimalException` levée                         |
| **Exigence**     | REQ-Z-005                                                |

---

### TC-006 — AddAnimal lève ZooCapacityExceededException quand la capacité (50) est atteinte

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 50 animaux Healthy ajoutés ; tentative d'ajout d'un 51ᵉ  |
| **Attendu**      | `ZooCapacityExceededException` levée                     |
| **Exigence**     | REQ-Z-006                                                |

---

### TC-007 — TotalCapacityUsed compte un animal Critical pour 2 places

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 1 animal `Healthy` + 1 animal `Critical`                 |
| **Attendu**      | `TotalCapacityUsed` retourne `3` (1 + 2)                 |
| **Exigence**     | REQ-Z-007                                                |

---

### TC-008 — CalculateDailyRation d'un carnivore Healthy est 5 kg

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `Animal { Category=Carnivore, Status=Healthy }`          |
| **Attendu**      | `CalculateDailyRation(id)` retourne `5.0`                |
| **Exigence**     | REQ-Z-008                                                |

---

### TC-009 — CalculateDailyRation d'un animal Sick est la ration de base − 30 %

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `Animal { Category=Carnivore, Status=Sick }` (base 5 kg) |
| **Attendu**      | `CalculateDailyRation(id)` retourne `3.5` (5 × 0.70)    |
| **Exigence**     | REQ-Z-009                                                |

---

### TC-010 — CalculateDailyCost retourne le coût total correct pour plusieurs animaux

| Champ            | Détail                                                                            |
|------------------|-----------------------------------------------------------------------------------|
| **Entrée**       | 1 carnivore Healthy (25 €) + 1 herbivore Healthy (8 €)                           |
| **Attendu**      | `CalculateDailyCost()` retourne `33.0`                                           |
| **Exigence**     | REQ-Z-010                                                                        |

---

### TC-011 — Un animal Sick ajoute +20 € au coût quotidien

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 1 carnivore `Sick` (25 € + 20 €)                         |
| **Attendu**      | `CalculateDailyCost()` retourne `45.0`                   |
| **Exigence**     | REQ-Z-011                                                |

---

### TC-012 — Un animal Critical ajoute +50 € au coût quotidien

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 1 carnivore `Critical` (25 € + 50 €)                     |
| **Attendu**      | `CalculateDailyCost()` retourne `75.0`                   |
| **Exigence**     | REQ-Z-012                                                |

---

### TC-013 — GetCriticalAnimals retourne uniquement les animaux Critical

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 1 Healthy + 1 Sick + 2 Critical                          |
| **Attendu**      | Liste de 2 éléments, tous avec `Status=Critical`         |
| **Exigence**     | REQ-Z-013                                                |

---

### TC-014 — RemoveAnimal retire l'animal et retourne true

| Champ            | Détail                                                         |
|------------------|----------------------------------------------------------------|
| **Entrée**       | Animal `Id=1` ajouté ; `RemoveAnimal(1)`                      |
| **Attendu**      | Retourne `true` ; `GetAnimal(1)` retourne ensuite `null`      |
| **Exigence**     | REQ-Z-014                                                     |

---

### TC-015 — RemoveAnimal retourne false pour un ID inexistant

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | `ZooManager` vide ; `RemoveAnimal(999)`                  |
| **Attendu**      | Retourne `false`                                         |
| **Exigence**     | REQ-Z-015                                                |

---

### TC-016 — TotalAnimals vaut 0 pour un ZooManager vide (cas limite)

| Champ            | Détail                                        |
|------------------|-----------------------------------------------|
| **Entrée**       | `ZooManager` fraîchement instancié            |
| **Attendu**      | `TotalAnimals` retourne `0`                   |
| **Exigence**     | REQ-Z-004 (cas limite)                        |

---

### TC-017 — TotalCapacityUsed vaut 0 pour un ZooManager vide (cas limite)

| Champ            | Détail                                        |
|------------------|-----------------------------------------------|
| **Entrée**       | `ZooManager` fraîchement instancié            |
| **Attendu**      | `TotalCapacityUsed` retourne `0`              |
| **Exigence**     | REQ-Z-007 (cas limite)                        |

---

### TC-018 — GetCriticalAnimals retourne une liste vide s'il n'y a aucun animal Critical

| Champ            | Détail                                                    |
|------------------|-----------------------------------------------------------|
| **Entrée**       | 2 animaux Healthy, 1 Sick                                |
| **Attendu**      | `GetCriticalAnimals()` retourne une liste vide           |
| **Exigence**     | REQ-Z-013 (cas limite)                                   |

---

### TC-019 — Un animal Critical bloque l'ajout si la capacité résiduelle est < 2

| Champ            | Détail                                                                         |
|------------------|--------------------------------------------------------------------------------|
| **Entrée**       | 49 animaux Healthy déjà présents (capacité utilisée = 49) ; ajout d'un animal `Critical` |
| **Attendu**      | `ZooCapacityExceededException` levée (Critical nécessite 2 places, il n'en reste qu'1) |
| **Exigence**     | REQ-Z-006 + REQ-Z-007                                                         |

---

## 8. Matrice de traçabilité

| Exigence  | Cas de test       | Statut prévu |
|-----------|-------------------|--------------|
| REQ-Z-001 | TC-001            | À faire      |
| REQ-Z-002 | TC-002            | À faire      |
| REQ-Z-003 | TC-003            | À faire      |
| REQ-Z-004 | TC-004, TC-016    | À faire      |
| REQ-Z-005 | TC-005            | À faire      |
| REQ-Z-006 | TC-006, TC-019    | À faire      |
| REQ-Z-007 | TC-007, TC-017, TC-019 | À faire |
| REQ-Z-008 | TC-008            | À faire      |
| REQ-Z-009 | TC-009            | À faire      |
| REQ-Z-010 | TC-010            | À faire      |
| REQ-Z-011 | TC-011            | À faire      |
| REQ-Z-012 | TC-012            | À faire      |
| REQ-Z-013 | TC-013, TC-018    | À faire      |
| REQ-Z-014 | TC-014            | À faire      |
| REQ-Z-015 | TC-015            | À faire      |

---

## 9. Risques

| Risque | Probabilité | Impact | Mitigation |
|--------|-------------|--------|------------|
|        |             |        |            |
