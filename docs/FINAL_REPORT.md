# FINAL REPORT — BankSystem

## 1. Опис проєкту

BankSystem — це консольний застосунок для моделювання базової банківської системи, створений у межах курсу об’єктно-орієнтованого програмування.

Проєкт демонструє використання принципів чистої архітектури, SOLID, шаблонів проєктування, тестування та persistence-механізмів.

Основна функціональність системи:

- створення акаунтів;
- поповнення балансу;
- зняття коштів;
- перевірка коректності транзакцій;
- збереження даних у JSON;
- відновлення даних після перезапуску;
- unit та integration testing.

---

# 2. Архітектура проєкту

Проєкт побудований за принципами Clean Architecture та поділений на окремі шари:

## Domain

Містить:
- бізнес-сутності;
- інтерфейси;
- базову доменну логіку.

Основні класи:
- Account;
- IRepository<T>.

---

## Application

Містить:
- бізнес-логіку;
- сервіси;
- LINQ-запити;
- Strategy Pattern.

Основні компоненти:
- BankService;
- IFeeStrategy;
- PremiumFeeStrategy;
- StandardFeeStrategy.

---

## Infrastructure

Містить:
- persistence logic;
- JsonRepository<T>;
- роботу з файловою системою.

---

## Tests

Містить:
- unit tests;
- integration tests;
- перевірку persistence;
- coverage testing.

---

# 3. Використані принципи та патерни

## SOLID

### Single Responsibility Principle
Кожен клас відповідає лише за одну задачу.

### Open/Closed Principle
Система може розширюватися новими fee strategies без зміни BankService.

### Dependency Inversion Principle
BankService працює через IRepository<T> та IFeeStrategy.

---

## Strategy Pattern

Використано для різних типів комісій:

- StandardFeeStrategy;
- PremiumFeeStrategy.

Це дозволяє легко додавати нові алгоритми.

---

## Generic Repository Pattern

JsonRepository<T> реалізує універсальне збереження даних.

---

# 4. Persistence

Для persistence використано JSON serialization через System.Text.Json.

Переваги:
- простота;
- читабельність;
- легке тестування;
- незалежність від СУБД.

Система підтримує:
- створення файлів;
- завантаження;
- збереження;
- обробку пустих файлів;
- обробку некоректного JSON.

---

# 5. Тестування

У проєкті реалізовано:

## Unit tests
Перевірка:
- логіки сервісів;
- fee strategies;
- бізнес-правил.

## Integration tests
Перевірка:
- JsonRepository;
- persistence;
- файлової системи;
- серіалізації.

---

## Coverage

Покриття тестами:

- Line coverage: 88.7%
- Branch coverage: 83.3%

Тести запускаються через:

```bash
dotnet test



````md
---

# 6. Рефакторинг

Під час фінального етапу було виконано цільовий рефакторинг проєкту.

Основні зміни:

- усунено дублювання коду;
- покращено читабельність назв методів і змінних;
- розділено відповідальності між шарами;
- покращено структуру тестів;
- винесено fee logic у Strategy Pattern;
- покращено організацію persistence logic.

Також були додані XML-коментарі до ключових публічних API.

---

# 7. Аналіз продуктивності

Було проведено базовий аналіз сценарію отримання та фільтрації акаунтів.

Для зберігання даних використовується:

- List<T> — для швидкого перебору та простоти роботи;
- LINQ — для фільтрації та агрегації.

Приклад:

```csharp
var richAccounts = accounts
    .Where(a => a.Balance >= 1000);
    8. UML та документація

У проєкті підготовлено:

UML-діаграми;
README;
USER_GUIDE;
DEVELOPER_GUIDE;
TESTING;
CHANGELOG;
FINAL_REPORT;
release-plan;
syllabus-coverage;
defense-qa.
9. Висновок

У результаті виконання лабораторного проєкту було створено повноцінний консольний застосунок із чистою архітектурою, persistence-механізмами, тестуванням та документацією.

Проєкт демонструє:

використання принципів ООП;
SOLID;
патернів проєктування;
LINQ;
generics;
persistence;
unit та integration testing;
рефакторинг і підтримуваність коду.