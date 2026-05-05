using System;
using BankSystem.Application;
using BankSystem.Domain;
using BankSystem.Infrastructure;

// Створюємо репозиторій, вказуючи назву файлу
var accountRepo = new JsonRepository<Account>("accounts.json");

// Створюємо сервіс (Application layer)
var bankService = new BankService(accountRepo);

Console.WriteLine("=== БАНКІВСЬКА СИСТЕМА v4.0 (Full Architecture) ===");

if (!bankService.GetAll().Any())
{
    Console.WriteLine("База порожня. Створюємо перші рахунки...");
    bankService.CreateAccount("Артем", 5000);
    bankService.CreateAccount("Олена", 1500);
}

// Демонстрація бізнес-логіки
try 
{
    Console.WriteLine("\nСпроба зняти 1000 грн у Артема (ID: 1):");
    bankService.Withdraw(1, 1000);
    Console.WriteLine("Успішно! Новий баланс: " + bankService.GetAll().First().Balance);
}
catch (Exception ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

Console.WriteLine("\nВсі клієнти в базі:");
foreach (var acc in bankService.GetAll())
{
    Console.WriteLine($"- {acc.OwnerName}: {acc.Balance} грн");
}