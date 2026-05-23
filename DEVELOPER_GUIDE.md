# Developer Guide

## Архітектура проекту

- `src/BankSystem.Domain` — доменні моделі та інтерфейси.
- `src/BankSystem.Application` — бізнес-логіка та операції.
- `src/BankSystem.Infrastructure` — реалізації сховищ (InMemoryRepository).
- `src/BankSystem.Console` — консольний інтерфейс.
- `tests/BankSystem.Tests` — unit-тести.

## Головні патерни

- Repository Pattern — інкапсулює доступ до даних;
- Layered Architecture — розділення домену, логіки та UI;
- Dependency Injection — передача репозиторію в `BankService`.

## Структура коду

- `Account` — доменна модель рахунку;
- `BankService` — бізнес-логіка для операцій над рахунками;
- `IRepository<T>` — базовий інтерфейс з методами `Add`, `GetById`, `GetAll`, `Update`, `SaveChanges`;
- `InMemoryRepository<T>` — сховище даних у пам'яті;
- `Program.cs` — консольне меню.

## Як розробляти

1. Відновіть пакети:
   ```bash
dotnet restore
   ```
2. Побудуйте проект:
   ```bash
dotnet build
   ```
3. Запустіть тести:
   ```bash
dotnet test
   ```

## Розширення

Для майбутніх релізів можна додати:
- JSON persistence;
- комісії через Strategy Pattern;
- LINQ-аналітику;
- інтеграційні тести;
- CI quality gate.
