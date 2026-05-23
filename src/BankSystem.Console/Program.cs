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

    var choice = ReadString("\nВаш вибір: ");

    try
    {
        switch (choice)
        {
            case "1":
                var name = ReadString("Ім'я власника: ");
                var balance = ReadDecimal("Початковий баланс: ");

                bankService.CreateAccount(name, balance);
                Console.WriteLine("Рахунок створено!");
                break;

            case "2":
                var depositId = ReadInt("ID рахунку: ");
                var depositAmount = ReadDecimal("Сума депозиту: ");

                bankService.Deposit(depositId, depositAmount);
                Console.WriteLine("Баланс оновлено!");
                break;

            case "3":
                var withdrawId = ReadInt("ID рахунку: ");
                var withdrawAmount = ReadDecimal("Сума: ");

                bankService.Withdraw(withdrawId, withdrawAmount);
                Console.WriteLine("Кошти знято!");
                break;

            case "4":
                var fromId = ReadInt("З якого рахунку: ");
                var toId = ReadInt("На який рахунок: ");
                var transferAmount = ReadDecimal("Сума: ");

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

static int ReadInt(string prompt)
{
    Console.Write(prompt);
    return int.Parse(Console.ReadLine()!);
}

static decimal ReadDecimal(string prompt)
{
    Console.Write(prompt);
    return decimal.Parse(Console.ReadLine()!);
}

static string ReadString(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()!;
}
