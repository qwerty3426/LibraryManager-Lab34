# Sequence Diagram

## Створення рахунку

1. Користувач вводить дані в `Program`.
2. `Program` викликає `BankService.CreateAccount()`.
3. `BankService` звертається до `IRepository<Account>.Add()`.
4. `InMemoryRepository<Account>` зберігає рахунок в пам’яті.

## Переказ коштів

1. Користувач вводить параметри переказу.
2. `Program` викликає `BankService.Transfer()`.
3. `BankService` отримує sender та receiver через `GetById()`.
4. `BankService` перевіряє баланс та виконує переміщення коштів.
5. `BankService` зберігає зміни через `SaveChanges()`.
