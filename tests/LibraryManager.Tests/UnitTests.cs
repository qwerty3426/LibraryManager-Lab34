using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.Application;
using BankSystem.Application.Strategies;
using BankSystem.Domain;
using Xunit;
using InMemoryRepo = BankSystem.Infrastructure.InMemoryRepository<BankSystem.Domain.Account>;

namespace LibraryManager.Tests;

public class UnitTests
{
    [Fact]
    public void CreateAccount_ShouldAddAccount()
    {
        var repo = new InMemoryRepo();
        var service = new BankService(repo);

        service.CreateAccount("Alice", 100m);

        var accounts = repo.GetAll().ToList();
        Assert.Single(accounts);
        Assert.Equal("Alice", accounts[0].OwnerName);
        Assert.Equal(100m, accounts[0].Balance);
    }

    [Fact]
    public void CreatePremiumAccount_ShouldSetAccountType()
    {
        var repo = new InMemoryRepo();
        var service = new BankService(repo);

        service.CreateAccount("Bob", 500m, "Premium");

        var account = repo.GetById(1);
        Assert.Equal("Premium", account.AccountType);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m });
        var service = new BankService(repo);

        service.Withdraw(1, 50m);

        Assert.Equal(150m, repo.GetById(1).Balance);
    }

    [Fact]
    public void Withdraw_InvalidAccount_ShouldThrow()
    {
        var repo = new InMemoryRepo();
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Withdraw(999, 10m));
        Assert.Contains("не знайдено", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Withdraw_InvalidAmount_ShouldThrow(decimal amount)
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m });
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Withdraw(1, amount));
        Assert.Contains("більшою за 0", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Withdraw_ExceedsLimit_ShouldThrow()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 30000m });
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Withdraw(1, 25000m));
        Assert.Contains("Перевищено ліміт", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Withdraw_InsufficientBalance_ShouldThrow()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 20m });
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Withdraw(1, 50m));
        Assert.Contains("Недостатньо коштів", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Transfer_ShouldMoveFunds()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 50m });
        var service = new BankService(repo);

        service.Transfer(1, 2, 100m);

        Assert.Equal(98m, repo.GetById(1).Balance); // 100 + 2 fee deducted
        Assert.Equal(150m, repo.GetById(2).Balance);
    }

    [Fact]
    public void Transfer_PremiumAccount_ShouldChargeZeroFee()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m, AccountType = "Premium" });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 50m });
        var service = new BankService(repo);

        service.Transfer(1, 2, 100m);

        Assert.Equal(100m, repo.GetById(1).Balance);
        Assert.Equal(150m, repo.GetById(2).Balance);
    }

    [Fact]
    public void Transfer_InvalidAccount_ShouldThrow()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m });
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Transfer(1, 999, 50m));
        Assert.Contains("не знайдено", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Transfer_InvalidAmount_ShouldThrow()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 200m });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 100m });
        var service = new BankService(repo);

        var exception = Assert.Throws<Exception>(() => service.Transfer(1, 2, 0m));
        Assert.Contains("більшою за 0", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetRichAccounts_ShouldReturnOnlyRichAccounts()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 500m });
        var service = new BankService(repo);

        var rich = service.GetRichAccounts(200m).ToList();

        Assert.Single(rich);
        Assert.Equal(2, rich[0].Id);
    }

    [Fact]
    public void GetTopAccounts_ShouldReturnTopFive()
    {
        var repo = new InMemoryRepo();
        for (var i = 1; i <= 10; i++)
        {
            repo.Add(new Account { Id = i, OwnerName = $"User{i}", Balance = i * 10m });
        }

        var service = new BankService(repo);
        var topAccounts = service.GetTopAccounts().ToList();

        Assert.Equal(5, topAccounts.Count);
        Assert.Equal(10, topAccounts[0].Id);
    }

    [Fact]
    public void GetTotalBalance_ShouldSumAllBalances()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 200m });
        var service = new BankService(repo);

        var total = service.GetTotalBalance();
        Assert.Equal(300m, total);
    }

    [Fact]
    public void StandardFeeStrategy_ShouldReturnMinimumFee()
    {
        var strategy = new StandardFeeStrategy();
        Assert.Equal(2m, strategy.CalculateFee(50m));
    }

    [Fact]
    public void StandardFeeStrategy_ShouldReturnPercentFee()
    {
        var strategy = new StandardFeeStrategy();
        Assert.Equal(5m, strategy.CalculateFee(500m));
    }

    [Fact]
    public void PremiumFeeStrategy_ShouldReturnZeroFee()
    {
        var strategy = new PremiumFeeStrategy();
        Assert.Equal(0m, strategy.CalculateFee(200m));
    }

    [Fact]
    public void Repository_GetById_ShouldReturnItem()
    {
        var repo = new InMemoryRepo();
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });

        var account = repo.GetById(1);

        Assert.NotNull(account);
        Assert.Equal("Alice", account.OwnerName);
    }

    [Fact]
    public void Repository_GetById_InvalidId_ShouldReturnNull()
    {
        var repo = new InMemoryRepo();

        var account = repo.GetById(99);

        Assert.Null(account);
    }

    [Fact]
    public void GetAllAccounts_WhenEmpty_ShouldReturnEmptyCollection()
    {
        var repo = new InMemoryRepo();
        var service = new BankService(repo);

        Assert.Empty(service.GetAllAccounts());
    }
}
