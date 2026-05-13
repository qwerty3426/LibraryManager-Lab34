using Xunit;
using Moq;
using FluentAssertions;
using BankSystem.Application;
using BankSystem.Domain;

namespace BankSystem.Tests.UnitTests;

public class BankServiceTests
{
    // ========================================
    // CREATE ACCOUNT
    // ========================================

    [Fact]
    public void CreateAccount_ShouldAddAccount()
    {
        var repoMock = CreateRepositoryMock(out var accounts);

        var service = new BankService(repoMock.Object);

        service.CreateAccount("Artem", 1000);

        accounts.Should().HaveCount(1);
        accounts[0].OwnerName.Should().Be("Artem");
        accounts[0].Balance.Should().Be(1000);
    }

    [Fact]
    public void CreateAccount_ShouldSetPremiumType()
    {
        var repoMock = CreateRepositoryMock(out var accounts);

        var service = new BankService(repoMock.Object);

        service.CreateAccount("Artem", 5000, AccountType.Premium);

        accounts[0].AccountType.Should().Be(AccountType.Premium);
    }

    // ========================================
    // WITHDRAW
    // ========================================

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        var account = new Account
        {
            Id = 1,
            OwnerName = "Artem",
            Balance = 1000
        };

        var repoMock = CreateRepositoryMock(out var accounts, account);

        var service = new BankService(repoMock.Object);

        service.Withdraw(1, 200);

        account.Balance.Should().Be(800);
    }

    [Fact]
    public void Withdraw_ShouldThrow_WhenAccountNotFound()
    {
        var repoMock = CreateRepositoryMock(out var accounts);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Withdraw(1, 100);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Withdraw_ShouldThrow_WhenBalanceTooLow()
    {
        var account = new Account
        {
            Id = 1,
            Balance = 100
        };

        var repoMock = CreateRepositoryMock(out var accounts, account);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Withdraw(1, 1000);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Withdraw_ShouldThrow_WhenAmountIsZero()
    {
        var account = new Account
        {
            Id = 1,
            Balance = 1000
        };

        var repoMock = CreateRepositoryMock(out var accounts, account);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Withdraw(1, 0);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Withdraw_ShouldThrow_WhenAmountIsNegative()
    {
        var account = new Account
        {
            Id = 1,
            Balance = 1000
        };

        var repoMock = CreateRepositoryMock(out var accounts, account);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Withdraw(1, -100);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Withdraw_ShouldThrow_WhenLimitExceeded()
    {
        var account = new Account
        {
            Id = 1,
            Balance = 50000
        };

        var repoMock = CreateRepositoryMock(out var accounts, account);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Withdraw(1, 30000);

        act.Should().Throw<Exception>();
    }

    // ========================================
    // TRANSFER
    // ========================================

    [Fact]
    public void Transfer_ShouldMoveMoney()
    {
        var acc1 = new Account
        {
            Id = 1,
            Balance = 1000
        };

        var acc2 = new Account
        {
            Id = 2,
            Balance = 500
        };

        var repoMock = CreateRepositoryMock(out var accounts, acc1, acc2);

        var service = new BankService(repoMock.Object);

        service.Transfer(1, 2, 200);

        acc1.Balance.Should().BeLessThan(1000);
        acc2.Balance.Should().Be(700);
    }

    [Fact]
    public void Transfer_ShouldThrow_WhenSenderMissing()
    {
        var acc2 = new Account
        {
            Id = 2,
            Balance = 500
        };

        var repoMock = CreateRepositoryMock(out var accounts, acc2);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Transfer(1, 2, 100);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Transfer_ShouldThrow_WhenReceiverMissing()
    {
        var acc1 = new Account
        {
            Id = 1,
            Balance = 1000
        };

        var repoMock = CreateRepositoryMock(out var accounts, acc1);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Transfer(1, 2, 100);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Transfer_ShouldThrow_WhenAmountNegative()
    {
        var acc1 = new Account { Id = 1, Balance = 1000 };
        var acc2 = new Account { Id = 2, Balance = 500 };

        var repoMock = CreateRepositoryMock(out var accounts, acc1, acc2);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Transfer(1, 2, -50);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Transfer_ShouldThrow_WhenNotEnoughMoney()
    {
        var acc1 = new Account { Id = 1, Balance = 100 };
        var acc2 = new Account { Id = 2, Balance = 500 };

        var repoMock = CreateRepositoryMock(out var accounts, acc1, acc2);

        var service = new BankService(repoMock.Object);

        Action act = () => service.Transfer(1, 2, 1000);

        act.Should().Throw<Exception>();
    }

    // ========================================
    // LINQ
    // ========================================

    [Fact]
    public void GetRichAccounts_ShouldReturnFilteredAccounts()
    {
        var acc1 = new Account { Id = 1, Balance = 100 };
        var acc2 = new Account { Id = 2, Balance = 5000 };

        var repoMock = CreateRepositoryMock(out var accounts, acc1, acc2);

        var service = new BankService(repoMock.Object);

        var result = service.GetRichAccounts(1000);

        result.Should().HaveCount(1);
    }

    [Fact]
    public void GetTopAccounts_ShouldReturnMaxFive()
    {
        var list = new List<Account>();

        for (int i = 1; i <= 10; i++)
        {
            list.Add(new Account
            {
                Id = i,
                Balance = i * 100
            });
        }

        var repoMock = CreateRepositoryMock(out var accounts, list.ToArray());

        var service = new BankService(repoMock.Object);

        var result = service.GetTopAccounts();

        result.Should().HaveCount(5);
    }

    [Fact]
    public void GetTotalBalance_ShouldReturnCorrectSum()
    {
        var acc1 = new Account { Balance = 100 };
        var acc2 = new Account { Balance = 200 };

        var repoMock = CreateRepositoryMock(out var accounts, acc1, acc2);

        var service = new BankService(repoMock.Object);

        var result = service.GetTotalBalance();

        result.Should().Be(300);
    }

    // ========================================
    // HELPER
    // ========================================

    private Mock<IRepository<Account>> CreateRepositoryMock(
        out List<Account> accounts,
        params Account[] initialAccounts)
    {
        var accountsLocal = initialAccounts.ToList();
        accounts = accountsLocal;

        var repoMock = new Mock<IRepository<Account>>();

        repoMock
            .Setup(r => r.GetAll())
            .Returns(accountsLocal);

        repoMock
            .Setup(r => r.Add(It.IsAny<Account>()))
            .Callback<Account>(a => accountsLocal.Add(a));

        repoMock
            .Setup(r => r.SaveChanges());

        return repoMock;
    }
}