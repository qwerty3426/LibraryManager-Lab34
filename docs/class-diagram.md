# Class Diagram

## Основні класи

- `Account` — представлення банківського рахунку.
- `BankService` — реалізація бізнес-логіки операцій над рахунком.
- `IRepository<T>` — інтерфейс для сховища даних.
- `InMemoryRepository<T>` — реалізація сховища в пам’яті.

## Зв’язки

- `BankService` використовує `IRepository<Account>`;
- `InMemoryRepository<Account>` реалізує `IRepository<Account>`;
- `Program` використовує `BankService` для виконання операцій.
