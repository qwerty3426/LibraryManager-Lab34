using BankSystem.Application;
using BankSystem.Domain;

var accountRepo = new InMemoryRepository<Account>();
var bankService = new BankService(accountRepo);

while (true)
{
    Console.WriteLine("\n===== BANK SYSTEM =====");
    Console.WriteLine("1. Створити рахунок");
    Console.WriteLine("2. Депозит");
    Console.WriteLine("3. Зняти кошти");
    Console.WriteLine("4. Переказ");
    Console.WriteLine("5. Всі рахунки");
    Console.WriteLine("0. Вихід");

    Console.Write("\nВаш вибір: ");
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Ім'я власника: ");
                var name = Console.ReadLine()!;

                Console.Write("Початковий баланс: ");
                var balance = decimal.Parse(Console.ReadLine()!);

                bankService.CreateAccount(name, balance);
                Console.WriteLine("Рахунок створено!");
                break;

            case "2":
                Console.Write("ID рахунку: ");
                var depositId = int.Parse(Console.ReadLine()!);

                Console.Write("Сума депозиту: ");
                var depositAmount = decimal.Parse(Console.ReadLine()!);

                bankService.Deposit(depositId, depositAmount);
                Console.WriteLine("Баланс оновлено!");
                break;

            case "3":
                Console.Write("ID рахунку: ");
                var withdrawId = int.Parse(Console.ReadLine()!);

                Console.Write("Сума: ");
                var withdrawAmount = decimal.Parse(Console.ReadLine()!);

                bankService.Withdraw(withdrawId, withdrawAmount);
                Console.WriteLine("Кошти знято!");
                break;

            case "4":
                Console.Write("З якого рахунку: ");
                var fromId = int.Parse(Console.ReadLine()!);

                Console.Write("На який рахунок: ");
                var toId = int.Parse(Console.ReadLine()!);

                Console.Write("Сума: ");
                var transferAmount = decimal.Parse(Console.ReadLine()!);

                bankService.Transfer(fromId, toId, transferAmount);
                Console.WriteLine("Переказ успішний!");
                break;

            case "5":
                var accounts = bankService.GetAllAccounts();
                foreach (var acc in accounts)
                {
                    Console.WriteLine($"ID: {acc.Id} | Owner: {acc.OwnerName} | Balance: {acc.Balance}");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір!");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}