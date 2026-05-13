using Xunit;
using Moq;
using FluentAssertions;
using BankSystem.Application;
using BankSystem.Domain;

namespace BankSystem.Tests.UnitTests;

public class BankServiceTests
{
    [Fact]
    public void CreateAccount_ShouldAddAccount()
    {
        // Arrange
        var accountRepoMock = new Mock<IRepository<Account>>();
        var transactionRepoMock = new Mock<IRepository<Transaction>>();

        var accounts = new List<Account>();

        accountRepoMock
            .Setup(r => r.GetAll())
            .Returns(accounts);

        accountRepoMock
            .Setup(r => r.Add(It.IsAny<Account>()))
            .Callback<Account>(a => accounts.Add(a));

        var service = new BankService(
            accountRepoMock.Object,
            transactionRepoMock.Object);

        // Act
        service.CreateAccount("Artem", 1000);

        // Assert
        accounts.Should().HaveCount(1);

        accounts[0].OwnerName.Should().Be("Artem");
        accounts[0].Balance.Should().Be(1000);
    }
}