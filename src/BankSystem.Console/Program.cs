using BankSystem.Application;
using BankSystem.Domain;

var accountRepo = new InMemoryRepository<Account>();

var bankService = new BankService(accountRepo);

while (true)
{
    Console.WriteLine("\n===== BANK SYSTEM =====");

    Console.WriteLine("1. Створити рахунок");
    Console.WriteLine("2. Зняти кошти");
    Console.WriteLine("3. Переказ");
    Console.WriteLine("4. Всі рахунки");
    Console.WriteLine("5. Багаті рахунки");
    Console.WriteLine("6. Топ-5 рахунків");
    Console.WriteLine("7. Загальний баланс");
    Console.WriteLine("0. Вихід");

    Console.Write("\nВаш вибір: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            // =========================
            // CREATE ACCOUNT
            // =========================

            case "1":

                Console.Write("Ім'я власника: ");
                string name = Console.ReadLine()!;

                Console.Write("Початковий баланс: ");
                decimal balance = decimal.Parse(Console.ReadLine()!);

                Console.Write("Тип акаунта (Standard/Premium): ");
                string type = Console.ReadLine()!;

                bankService.CreateAccount(
                    name,
                    balance,
                    type);

                Console.WriteLine("Рахунок створено!");

                break;

            // =========================
            // WITHDRAW
            // =========================

            case "2":

                Console.Write("ID рахунку: ");
                int withdrawId = int.Parse(Console.ReadLine()!);

                Console.Write("Сума: ");
                decimal withdrawAmount =
                    decimal.Parse(Console.ReadLine()!);

                bankService.Withdraw(
                    withdrawId,
                    withdrawAmount);

                Console.WriteLine("Кошти знято!");

                break;

            // =========================
            // TRANSFER
            // =========================

            case "3":

                Console.Write("З якого рахунку: ");
                int fromId = int.Parse(Console.ReadLine()!);

                Console.Write("На який рахунок: ");
                int toId = int.Parse(Console.ReadLine()!);

                Console.Write("Сума: ");
                decimal transferAmount =
                    decimal.Parse(Console.ReadLine()!);

                bankService.Transfer(
                    fromId,
                    toId,
                    transferAmount);

                Console.WriteLine("Переказ успішний!");

                break;

            // =========================
            // ALL ACCOUNTS
            // =========================

            case "4":

                var accounts =
                    bankService.GetAllAccounts();

                foreach (var acc in accounts)
                {
                    Console.WriteLine(
                        $"ID: {acc.Id} | " +
                        $"Owner: {acc.OwnerName} | " +
                        $"Balance: {acc.Balance} грн | " +
                        $"Type: {acc.AccountType}");
                }

                break;

            // =========================
            // RICH ACCOUNTS
            // =========================

            case "5":

                Console.Write("Мінімальний баланс: ");

                decimal min =
                    decimal.Parse(Console.ReadLine()!);

                var richAccounts =
                    bankService.GetRichAccounts(min);

                foreach (var acc in richAccounts)
                {
                    Console.WriteLine(
                        $"{acc.OwnerName} : {acc.Balance}");
                }

                break;

            // =========================
            // TOP ACCOUNTS
            // =========================

            case "6":

                var topAccounts =
                    bankService.GetTopAccounts();

                foreach (var acc in topAccounts)
                {
                    Console.WriteLine(
                        $"{acc.OwnerName} : {acc.Balance}");
                }

                break;

            // =========================
            // TOTAL BALANCE
            // =========================

            case "7":

                decimal total =
                    bankService.GetTotalBalance();

                Console.WriteLine(
                    $"Загальний баланс банку: {total} грн");

                break;

            // =========================
            // EXIT
            // =========================

            case "0":

                return;

            default:

                Console.WriteLine("Невірний вибір!");

                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Помилка: {ex.Message}");
    }
}