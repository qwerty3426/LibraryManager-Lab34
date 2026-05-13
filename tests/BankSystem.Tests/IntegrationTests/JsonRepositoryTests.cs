using Xunit;
using FluentAssertions;
using BankSystem.Domain;
using BankSystem.Infrastructure;

namespace BankSystem.Tests.IntegrationTests;

public class JsonRepositoryTests
{
    [Fact]
    public void Repository_ShouldSaveAccountToJson()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "[]");

        var repo = new JsonRepository<Account>(path);

        var account = new Account
        {
            Id = 1,
            OwnerName = "Artem",
            Balance = 1000
        };

        // Act
        repo.Add(account);
        repo.SaveChanges();

        // Assert
        File.Exists(path).Should().BeTrue();

        var content = File.ReadAllText(path);

        content.Should().Contain("Artem");
        content.Should().Contain("1000");

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldLoadAccountsFromJson()
    {
        // Arrange
        string path = Path.GetTempFileName();

        var json = """
        [
            {
                "Id": 1,
                "OwnerName": "Artem",
                "Balance": 5000
            }
        ]
        """;

        File.WriteAllText(path, json);

        // Act
        var repo = new JsonRepository<Account>(path);

        var accounts = repo.GetAll();

        // Assert
        accounts.Should().HaveCount(1);

        accounts.First().OwnerName.Should().Be("Artem");

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldHandleEmptyFile()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "[]");

        // Act
        var repo = new JsonRepository<Account>(path);

        var accounts = repo.GetAll();

        // Assert
        accounts.Should().NotBeNull();

        accounts.Should().BeEmpty();

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldHandleBrokenJson()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "{BROKEN JSON");

        // Act
        var repo = new JsonRepository<Account>(path);

        var result = repo.GetAll().ToList();

        // Assert
        result.Should().BeEmpty();

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldCreateFileIfMissing()
    {
        // Arrange
        string path = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid() + ".json");

        var repo = new JsonRepository<Account>(path);

        // Act
        repo.Add(new Account
        {
            Id = 1,
            OwnerName = "Test",
            Balance = 100
        });

        repo.SaveChanges();

        // Assert
        File.Exists(path).Should().BeTrue();

        var content = File.ReadAllText(path);

        content.Should().Contain("Test");

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldStoreMultipleAccounts()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "[]");

        var repo = new JsonRepository<Account>(path);

        // Act
        repo.Add(new Account
        {
            Id = 1,
            OwnerName = "A",
            Balance = 100
        });

        repo.Add(new Account
        {
            Id = 2,
            OwnerName = "B",
            Balance = 200
        });

        repo.SaveChanges();

        // Assert
        var content = File.ReadAllText(path);

        content.Should().Contain("A");
        content.Should().Contain("B");

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldReturnAllAccounts()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "[]");

        var repo = new JsonRepository<Account>(path);

        repo.Add(new Account
        {
            Id = 1,
            OwnerName = "User1"
        });

        repo.Add(new Account
        {
            Id = 2,
            OwnerName = "User2"
        });

        repo.SaveChanges();

        // Act
        var result = repo.GetAll();

        // Assert
        result.Should().HaveCount(2);

        File.Delete(path);
    }

    [Fact]
    public void Repository_ShouldPersistDataBetweenInstances()
    {
        // Arrange
        string path = Path.GetTempFileName();

        File.WriteAllText(path, "[]");

        var repo1 = new JsonRepository<Account>(path);

        repo1.Add(new Account
        {
            Id = 1,
            OwnerName = "Persist"
        });

        repo1.SaveChanges();

        // Act
        var repo2 = new JsonRepository<Account>(path);

        var result = repo2.GetAll();

        // Assert
        result.Should().ContainSingle();

        result.First().OwnerName.Should().Be("Persist");

        File.Delete(path);
    }
}