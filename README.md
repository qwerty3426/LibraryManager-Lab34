# BankSystem

BankSystem — консольний застосунок для моделювання банківської системи, створений у межах курсу ООП.

Проєкт демонструє:

- Clean Architecture;
- SOLID principles;
- Strategy Pattern;
- Generic Repository Pattern;
- JSON persistence;
- Unit та Integration Testing;
- LINQ;
- рефакторинг і тестове покриття.

---

# Основні можливості

## Робота з акаунтами

- створення акаунтів;
- поповнення балансу;
- зняття коштів;
- перевірка коректності операцій.

## Persistence

- збереження даних у JSON;
- автоматичне відновлення після перезапуску;
- обробка пошкоджених файлів.

## Тестування

- unit tests;
- integration tests;
- coverage report.

---

# Архітектура проєкту

Проєкт поділений на окремі шари:

```text
src/
 ├── BankSystem.Domain
 ├── BankSystem.Application
 └── BankSystem.Infrastructure

tests/
 └── BankSystem.Tests
```

---

# Використані технології

- C#
- .NET 9
- xUnit
- FluentAssertions
- System.Text.Json
- ReportGenerator
- Coverlet

---

# Запуск проєкту

## Клонування репозиторію

```bash
git clone https://github.com/qwerty3426/LibraryManager-Lab34.git
```

---

## Запуск застосунку

```bash
dotnet run --project src/BankSystem.Console
```

---

# Запуск тестів

```bash
dotnet test
```

---

# Генерація coverage report

```bash
dotnet test --collect:"XPlat Code Coverage"
```

Після цього:

```bash
reportgenerator -reports:"tests/BankSystem.Tests/TestResults/**/coverage.cobertura.xml" -targetdir:"coverage-report"
```

Відкрити звіт:

```bash
start coverage-report/index.html
```

---

# Покриття тестами

- Line coverage: 88.7%
- Branch coverage: 83.3%

---

# Документація

- [USER_GUIDE.md](USER_GUIDE.md)
- [DEVELOPER_GUIDE.md](DEVELOPER_GUIDE.md)
- [TESTING.md](TESTING.md)
- [CHANGELOG.md](CHANGELOG.md)
- [FINAL_REPORT.md](FINAL_REPORT.md)

---

# UML та додаткові матеріали

Усі UML-артефакти та додаткові документи знаходяться в папці:

```text
docs/
```

---

# Автор

Artem Kotsiuba  
Rivne Professional College of Information Technologies