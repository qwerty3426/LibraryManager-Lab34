using Xunit;
using BankSystem.Application;
using BankSystem.Domain;
using System.Linq;

namespace BankSystem.Tests
{
    public class BankTests
    {
        [Fact] // Unit Test
        public void Withdraw_ValidAmount_DecreasesBalance() {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);
            service.CreateAccount("Artem", 1000);
            
            service.Withdraw(1, 400);
            
            Assert.Equal(600, service.GetAll().First().Balance);
        }

        [Fact] // Negative Scenario Test
        public void Withdraw_TooMuch_ThrowsException() {
            var repo = new InMemoryRepository<Account>();
            var service = new BankService(repo);
            service.CreateAccount("Artem", 100);
            
            Assert.Throws<InvalidOperationException>(() => service.Withdraw(1, 500));
        }
    }
}