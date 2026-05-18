# DEVELOPER GUIDE — BankSystem

# 1. Архітектура проєкту

Проєкт побудований за принципами Clean Architecture.

Структура:

```text
src/
 ├── BankSystem.Domain
 ├── BankSystem.Application
 ├── BankSystem.Infrastructure
 └── BankSystem.Console

tests/
 └── BankSystem.Tests
```

---

# 2. Призначення шарів

## Domain

Містить:
- сутності;
- інтерфейси;
- базову бізнес-логіку.

Основні компоненти:
- Account;
- IRepository<T>.

---

## Application

Містить:
- бізнес-сервіси;
- LINQ;
- Strategy Pattern;
- логіку транзакцій.

Основні компоненти:
- BankService;
- IFeeStrategy;
- PremiumFeeStrategy;
- StandardFeeStrategy.

---

## Infrastructure

Містить:
- JsonRepository<T>;
- persistence logic;
- роботу з файловою системою.

---

## Tests

Містить:
- unit tests;
- integration tests;
- coverage tests.

---

# 3. Принципи проєктування

## SOLID

У проєкті використано:

- SRP;
- OCP;
- DIP.

---

## Strategy Pattern

Використовується для реалізації різних алгоритмів комісії.

Нові стратегії можна додавати без зміни існуючого коду.

---

## Generic Repository Pattern

JsonRepository<T> забезпечує універсальну роботу зі збереженням даних.

---

# 4. Persistence

Persistence реалізований через:

```text
System.Text.Json
```

Підтримується:
- серіалізація;
- десеріалізація;
- створення файлів;
- обробка помилок.

---

# 5. Запуск проєкту

## Запуск застосунку

```bash
dotnet run --project src/BankSystem.Console
```

---

## Запуск тестів

```bash
dotnet test
```

---

# 6. Coverage

Генерація coverage report:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

```bash
reportgenerator -reports:"tests/BankSystem.Tests/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report"
```

---

# 7. Розширення системи

Для додавання нового функціоналу рекомендується:

- дотримуватись SOLID;
- не змішувати Domain та Infrastructure;
- створювати окремі strategy-класи;
- покривати нову логіку тестами.

---

# 8. Рекомендації для розробників

- використовувати meaningful naming;
- уникати дублювання;
- писати невеликі методи;
- підтримувати test coverage;
- регулярно виконувати рефакторинг.