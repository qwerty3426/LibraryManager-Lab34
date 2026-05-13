using System;
using System.Linq;
using Xunit;
using BankSystem.Domain;
using BankSystem.Application;

namespace BankSystem.Tests
{
    public class BankServiceTests
    {
        // =========================
        // СТВОРЕННЯ РАХУНКУ
        // =========================
        [Fact]
        public void CreateAccount_Should_Add_New_Account()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 1000);

            Assert.Single(repo.GetAll());
        }

        // =========================
        // ЗНЯТТЯ КОШТІВ
        // =========================
        [Fact]
        public void Withdraw_Should_Decrease_Balance()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 1000);
            service.Withdraw(1, 200);

            var acc = repo.GetAll().First();
            Assert.Equal(800, acc.Balance);
        }

        [Fact]
        public void Withdraw_Should_Throw_When_Amount_Is_Negative()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 1000);

            Assert.Throws<Exception>(() => service.Withdraw(1, -100));
        }

        [Fact]
        public void Withdraw_Should_Throw_When_Limit_Exceeded()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 50000);

            // Очікуємо помилку, бо ліміт у нас 20,000
            Assert.Throws<Exception>(() => service.Withdraw(1, 30000));
        }

        // =========================
        // ПЕРЕКАЗИ (TRANSFER)
        // =========================
        [Fact]
        public void Transfer_Should_Move_Money()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("A", 5000); 
            service.CreateAccount("B", 1000);

            service.Transfer(1, 2, 200);

            var accounts = repo.GetAll().ToList();
            Assert.True(accounts[0].Balance < 5000);
            Assert.Equal(1200, accounts[1].Balance);
        }

        [Fact]
        public void Transfer_Should_Throw_When_No_Money()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("A", 100);
            service.CreateAccount("B", 1000);

            Assert.Throws<Exception>(() => service.Transfer(1, 2, 500));
        }

        // =========================
        // СТРАТЕГІЇ КОМІСІЙ (STRATEGY)
        // =========================
        [Fact]
        public void Premium_Account_Should_Have_No_Fee()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Premium", 5000, AccountType.Premium);
            service.CreateAccount("Receiver", 1000);

            service.Transfer(1, 2, 100);

            var sender = repo.GetAll().First(a => a.Id == 1);
            Assert.Equal(4900, sender.Balance); // 5000 - 100 = 4900
        }

        [Fact]
        public void Standard_Account_Should_Have_Fee()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Standard", 1000, AccountType.Standard);
            service.CreateAccount("Receiver", 1000);

            service.Transfer(1, 2, 100);

            var sender = repo.GetAll().First(a => a.Id == 1);
            // Повинно бути менше 900 через комісію
            Assert.True(sender.Balance < 900);
        }

        // =========================
        // АНАЛІТИКА (LINQ)
        // =========================
        [Fact]
        public void GetRichAccounts_Should_Return_Filtered()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("A", 100);
            service.CreateAccount("B", 5000);

            var rich = service.GetRichAccounts(1000);

            Assert.Single(rich);
        }

        [Fact]
        public void TotalBalance_Should_Be_Correct()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("A", 100);
            service.CreateAccount("B", 200);

            decimal total = service.GetTotalBalance();

            Assert.Equal(300, total);
        }
    }
}