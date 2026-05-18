# TESTING — BankSystem

# 1. Загальна інформація

У проєкті реалізовано:

- unit testing;
- integration testing;
- coverage testing.

Для тестування використано:

- xUnit;
- FluentAssertions;
- Coverlet;
- ReportGenerator.

---

# 2. Unit Tests

Unit tests перевіряють:

- бізнес-логіку;
- fee strategies;
- валідацію;
- роботу сервісів;
- LINQ-запити.

Основні класи:
- BankServiceTests;
- FeeStrategyTests.

---

# 3. Integration Tests

Integration tests перевіряють:

- JsonRepository<T>;
- persistence;
- файлову систему;
- JSON serialization;
- відновлення даних.

Основний клас:
- JsonRepositoryTests.

---

# 4. Запуск тестів

Запуск усіх тестів:

```bash
dotnet test
```

---

# 5. Генерація coverage report

## Крок 1

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## Крок 2

```bash
reportgenerator -reports:"tests/BankSystem.Tests/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report"
```

---

## Крок 3

Відкрити coverage report:

```bash
start coverage-report/index.html
```

---

# 6. Результати покриття

## Line Coverage

- 88.7%

## Branch Coverage

- 83.3%

---

# 7. Що перевіряється тестами

## BankService

- створення акаунтів;
- поповнення;
- зняття коштів;
- fee strategies;
- перевірка помилок.

---

## JsonRepository<T>

- створення JSON-файлів;
- збереження даних;
- завантаження;
- робота з пустими файлами;
- обробка некоректного JSON.

---

# 8. Надійність системи

Тестове покриття підтверджує:
- стабільність бізнес-логіки;
- коректність persistence;
- правильну обробку помилок;
- працездатність архітектури.