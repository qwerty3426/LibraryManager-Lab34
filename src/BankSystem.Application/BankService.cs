using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using BankSystem.Domain;

namespace BankSystem.Application
{
    public class BankService
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly string _filePath = "accounts.json";

        public BankService(IRepository<Account> accountRepo, IRepository<Transaction> transactionRepo)
        {
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
            Load();
        }

        public void CreateAccount(string name, decimal initialBalance)
        {
            var id = _accountRepo.GetAll().Any() ? _accountRepo.GetAll().Max(a => a.Id) + 1 : 1;
            _accountRepo.Add(new Account { Id = id, OwnerName = name, Balance = initialBalance });
            Save();
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _accountRepo.GetAll().FirstOrDefault(a => a.Id == accountId);
            
            if (account == null) throw new Exception("Рахунок не знайдено!");
            if (account.Balance < amount) throw new InvalidOperationException("Недостатньо коштів на рахунку!");

            account.Balance -= amount;
            _transactionRepo.Add(new Transaction { Id = _transactionRepo.GetAll().Count() + 1, AccountId = accountId, Amount = -amount, Date = DateTime.Now });
            Save();
        }

        public IEnumerable<Account> GetAllAccounts() => _accountRepo.GetAll();

        public IEnumerable<Account> GetRichAccounts(decimal minBalance) => 
            _accountRepo.GetAll().Where(a => a.Balance >= minBalance);

        private void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_accountRepo.GetAll(), options);
            File.WriteAllText(_filePath, json);
        }

        private void Load()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json)) return;

                try 
                {
                    var options = new JsonSerializerOptions { 
                        PropertyNameCaseInsensitive = true,
                        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
                    };
                    
                    var accounts = JsonSerializer.Deserialize<List<Account>>(json, options);
                    if (accounts != null)
                    {
                        foreach (var acc in accounts) _accountRepo.Add(acc);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Попередження: Не вдалося прочитати файл даних: {ex.Message}");
                }
            }
        }
    }
}