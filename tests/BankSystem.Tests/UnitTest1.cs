using System;
using Xunit;
using BankSystem.Domain;
using BankSystem.Application;

namespace BankSystem.Tests
{
    public class BankServiceTests
    {
        [Fact]
        public void CreateAccount_Should_Add_New_Account()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 1000m);

            Assert.Single(repo.GetAll());
            Assert.Equal("Artem", repo.GetById(1).OwnerName);
        }

        [Fact]
        public void Deposit_Should_Increase_Balance()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 500m);
            service.Deposit(1, 200m);

            Assert.Equal(700m, repo.GetById(1).Balance);
        }

        [Fact]
        public void Withdraw_Should_Decrease_Balance()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 500m);
            service.Withdraw(1, 200m);

            Assert.Equal(300m, repo.GetById(1).Balance);
        }

        [Fact]
        public void Withdraw_InvalidAmount_Should_Throw()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Artem", 500m);

            Assert.Throws<Exception>(() => service.Withdraw(1, -50m));
        }

        [Fact]
        public void Transfer_Should_Move_Money()
        {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);

            service.CreateAccount("Alice", 500m);
            service.CreateAccount("Bob", 200m);

            service.Transfer(1, 2, 100m);

            Assert.Equal(400m, repo.GetById(1).Balance);
            Assert.Equal(300m, repo.GetById(2).Balance);
        }
    }
}
