# LibraryManager — Lab 34–36

Консольна система управління бібліотекою, реалізована в рамках дисципліни «Об’єктно-орієнтоване програмування». Проєкт розроблявся поетапно:
- Lab 34 — базова архітектура та бізнес-логіка;
- Lab 35 — persistence layer, LINQ та розширення функціоналу;
- Lab 36 — quality gate, автоматизоване тестування та fault handling.

---

# 🔹 Основні можливості системи

## Функціонал
- створення книг;
- видача книг користувачам;
- повернення книг;
- перевірка доступності;
- збереження даних у JSON;
- відновлення стану після перезапуску;
- LINQ-аналітика;
- автоматизоване тестування.

---

# 🔹 Архітектура проєкту

Проєкт побудований за принципами:
- SOLID;
- Separation of Concerns;
- багатошарової архітектури;
- Dependency Injection.

## Структура

### Domain
Містить:
- сутності;
- інтерфейси;
- бізнес-правила;
- доменні інваріанти.

### Application
Містить:
- бізнес-логіку;
- use cases;
- сервіси;
- LINQ-запити.

### Infrastructure
Містить:
- persistence layer;
- JSON serialization;
- файлове збереження.

### Console
Містить:
- консольне меню;
- взаємодію з користувачем.

### Tests
Містить:
- unit tests;
- integration tests;
- fault handling tests.

---

# 🔹 Бізнес-правила

У системі реалізовано:
- неможливість видачі недоступної книги;
- перевірку коректності даних;
- обробку помилкових сценаріїв;
- захист від дублювання;
- контроль стану об’єктів;
- fault handling для persistence layer.

---

# 🔹 Persistence Layer

Persistence реалізований через JSON.

Підтримується:
- файлове збереження;
- завантаження з JSON;
- обробка помилок I/O;
- перевірка пошкоджених файлів;
- відновлення стану системи.

---

# 🔹 LINQ та аналітика

Реалізовано:
- пошук;
- сортування;
- фільтрацію;
- агреговану статистику.

---

# 🔹 Quality Gate (Lab 36)

У рамках третьої ітерації реалізовано:

## Unit Testing
Додано понад 20 unit tests для:
- бізнес-логіки;
- доменних правил;
- прикордонних значень;
- fault handling;
- LINQ;
- негативних сценаріїв.

## Integration Testing
Додано 8 integration tests для:
- persistence layer;
- JSON save/load;
- reload state;
- sequential operations;
- invalid JSON;
- missing files.

## Fault Handling
Перевіряються:
- пошкоджені JSON-файли;
- порожні файли;
- помилки вводу;
- відсутні дані;
- некоректні стани.

## Coverage
Підключено:
- coverlet.msbuild;
- coverage collection;
- quality gate для CI.

## GitHub Actions
CI pipeline автоматично:
- збирає проєкт;
- запускає тести;
- перевіряє pull requests;
- виконує quality gate.

---

# 🔹 Технології

- C#
- .NET 9
- xUnit
- LINQ
- JSON
- GitHub Actions
- Coverlet

---

# 🔹 Документація

Оновлено:
- TESTING.md;
- docs/test-strategy.md;
- docs/test-matrix.md;
- docs/iteration-3.md.

---

# 🔹 Запуск проєкту

## Збірка

```bash
dotnet build
```

## Запуск

```bash
dotnet run --project src/BankSystem.Console
```

## Запуск тестів

```bash
dotnet test
```

## Coverage

```bash
dotnet test /p:CollectCoverage=true
```

---

# 🔹 Результат тестування

Реальна тестова база: 20+ unit tests та 8 integration tests.

---

# 🔹 Підготовка до Lab 37

Після завершення Lab 36 проєкт має:
- quality gate;
- автоматизоване тестування;
- coverage;
- fault handling;
- підготовлену тестову базу для фінальної ітерації.
