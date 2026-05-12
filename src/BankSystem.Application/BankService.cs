using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Domain;
using BankSystem.Application.Strategies;

namespace BankSystem.Application
{
    public class BankService
    {
        private readonly IRepository<Account> _accountRepo;

        public BankService(IRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }

        // =========================
        // Strategy Pattern
        // =========================

        private IFeeStrategy GetFeeStrategy(Account account)
        {
            if (account.AccountType == "Premium")
            {
                return new PremiumFeeStrategy();
            }

            return new StandardFeeStrategy();
        }

        // =========================
        // Створення рахунку
        // =========================

        public void CreateAccount(
            string name,
            decimal initialBalance,
            string accountType = "Standard")
        {
            var id = _accountRepo.GetAll().Any()
                ? _accountRepo.GetAll().Max(a => a.Id) + 1
                : 1;

            var account = new Account
            {
                Id = id,
                OwnerName = name,
                Balance = initialBalance,
                AccountType = accountType
            };

            _accountRepo.Add(account);

            _accountRepo.SaveChanges();
        }

        // =========================
        // Зняття коштів
        // =========================

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _accountRepo
                .GetAll()
                .FirstOrDefault(a => a.Id == accountId);

            if (account == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            if (amount <= 0)
            {
                throw new Exception("Сума повинна бути більшою за 0!");
            }

            // Добовий ліміт
            if (amount > 20000)
            {
                throw new Exception("Перевищено ліміт зняття!");
            }

            if (account.Balance < amount)
            {
                throw new Exception("Недостатньо коштів!");
            }

            account.Balance -= amount;

            _accountRepo.SaveChanges();
        }

        // =========================
        // Переказ між рахунками
        // =========================

        public void Transfer(
            int fromAccountId,
            int toAccountId,
            decimal amount)
        {
            var fromAcc = _accountRepo
                .GetAll()
                .FirstOrDefault(a => a.Id == fromAccountId);

            var toAcc = _accountRepo
                .GetAll()
                .FirstOrDefault(a => a.Id == toAccountId);

            if (fromAcc == null || toAcc == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            if (amount <= 0)
            {
                throw new Exception("Сума повинна бути більшою за 0!");
            }

            // =========================
            // Strategy Pattern
            // =========================

            var strategy = GetFeeStrategy(fromAcc);

            decimal fee = strategy.CalculateFee(amount);

            decimal total = amount + fee;

            // =========================
            // Бізнес-правило
            // =========================

            if (fromAcc.Balance < total)
            {
                throw new Exception("Недостатньо коштів з урахуванням комісії!");
            }

            // =========================
            // Переказ
            // =========================

            fromAcc.Balance -= total;

            toAcc.Balance += amount;

            _accountRepo.SaveChanges();
        }

        // =========================
        // Отримати всі рахунки
        // =========================

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepo.GetAll();
        }

        // =========================
        // LINQ — багаті рахунки
        // =========================

        public IEnumerable<Account> GetRichAccounts(decimal minBalance)
        {
            return _accountRepo
                .GetAll()
                .Where(a => a.Balance >= minBalance);
        }

        // =========================
        // LINQ — топ рахунків
        // =========================

        public IEnumerable<Account> GetTopAccounts()
        {
            return _accountRepo
                .GetAll()
                .OrderByDescending(a => a.Balance)
                .Take(5);
        }

        // =========================
        // LINQ — загальний баланс
        // =========================

        public decimal GetTotalBalance()
        {
            return _accountRepo
                .GetAll()
                .Sum(a => a.Balance);
        }

    }
}