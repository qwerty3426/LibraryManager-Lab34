# Syllabus Coverage — BankSystem

# 1. Основи ООП

## Використано

- класи;
- об’єкти;
- інкапсуляція;
- композиція;
- модульність.

## Реалізація

- Account;
- BankService;
- JsonRepository<T>.

---

# 2. Абстракції, інтерфейси, поліморфізм

## Використано

- інтерфейси;
- абстракції;
- поліморфізм.

## Реалізація

- IRepository<T>;
- IFeeStrategy;
- різні fee strategies.

---

# 3. Generics, колекції, LINQ, делегати

## Використано

- generic classes;
- List<T>;
- LINQ queries.

## Реалізація

- JsonRepository<T>;
- GetRichAccounts();
- фільтрація та агрегація.

---

# 4. Обробка помилок і persistence

## Використано

- try/catch;
- exception handling;
- JSON persistence.

## Реалізація

- JsonRepository<T>;
- validation logic;
- error handling.

---

# 5. SOLID

## Використано

- SRP;
- OCP;
- DIP.

## Реалізація

- розділення шарів;
- repository abstraction;
- strategy abstraction.

---

# 6. Патерни проєктування

## Використано

### Strategy Pattern

- PremiumFeeStrategy;
- StandardFeeStrategy.

### Generic Repository Pattern

- JsonRepository<T>.

---

# 7. UML

## Частково використано

Створено:
- UML class diagram;
- architecture diagram.

---

# 8. Тестування

## Використано

- unit testing;
- integration testing;
- coverage testing.

## Реалізація

- xUnit;
- FluentAssertions;
- Coverlet;
- ReportGenerator.

---

# 9. Рефакторинг

## Використано

- покращення naming;
- усунення дублювання;
- decomposition methods;
- code cleanup.

---

# 10. Додаткові можливості

## Реалізовано додатково

- coverage report;
- persistence recovery;
- JSON validation;
- release documentation.