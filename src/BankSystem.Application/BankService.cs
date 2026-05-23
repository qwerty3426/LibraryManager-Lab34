using System;
using System.Collections.Generic;
using BankSystem.Domain;

namespace BankSystem.Application
{
    public class BankService
    {
        private readonly IRepository<Account> _accountRepo;

        public BankService(IRepository<Account> accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public void CreateAccount(string name, decimal initialBalance)
        {
            int id = 1;

            foreach (var existing in _accountRepo.GetAll())
            {
                if (existing.Id >= id)
                {
                    id = existing.Id + 1;
                }
            }

            var account = new Account
            {
                Id = id,
                OwnerName = name,
                Balance = initialBalance
            };

            _accountRepo.Add(account);
            _accountRepo.SaveChanges();
        }

        public void Deposit(int accountId, decimal amount)
        {
            var account = _accountRepo.GetById(accountId);

            if (account == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            if (amount <= 0)
            {
                throw new Exception("Сума має бути більшою за 0!");
            }

            account.Balance += amount;
            _accountRepo.SaveChanges();
        }

        public void Withdraw(int accountId, decimal amount)
        {
            var account = _accountRepo.GetById(accountId);

            if (account == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            if (amount <= 0)
            {
                throw new Exception("Сума має бути більшою за 0!");
            }

            if (account.Balance < amount)
            {
                throw new Exception("Недостатньо коштів!");
            }

            account.Balance -= amount;
            _accountRepo.SaveChanges();
        }

        public void Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var sender = _accountRepo.GetById(fromAccountId);
            var receiver = _accountRepo.GetById(toAccountId);

            if (sender == null || receiver == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            if (amount <= 0)
            {
                throw new Exception("Сума має бути більшою за 0!");
            }

            if (sender.Balance < amount)
            {
                throw new Exception("Недостатньо коштів!");
            }

            sender.Balance -= amount;
            receiver.Balance += amount;
            _accountRepo.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepo.GetAll();
        }
    }
} 