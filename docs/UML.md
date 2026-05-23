# UML — BankSystem

## Основні сутності

### Account
- IBAN
- Balance
- OwnerName

### BankService
- виконання операцій з рахунками;
- перевірка балансу;
- керування переказами.

### IRepository<T>
- базовий інтерфейс для сховища;
- методи `Add`, `GetById`, `GetAll`, `Update`.

### InMemoryRepository<T>
- реалізація сховища в пам’яті;
- зберігає дані під час виконання програми.

## Архітектура

- `BankSystem.Domain` — доменні моделі;
- `BankSystem.Application` — бізнес-логіка;
- `BankSystem.Console` — інтерфейс користувача.
