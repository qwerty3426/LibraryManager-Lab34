using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankSystem.Domain;
using BankSystem.Infrastructure;
using Xunit;

namespace LibraryManager.Tests;

public class IntegrationTests : IDisposable
{
    private readonly string _filePath;

    public IntegrationTests()
    {
        _filePath = Path.Combine(Path.GetTempPath(), $"librarymanager_integration_{Guid.NewGuid()}.json");
    }

    public void Dispose()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
        }
    }

    [Fact]
    public void SaveFile_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });
        repo.SaveChanges();

        Assert.True(File.Exists(_filePath));
        Assert.Contains("Alice", File.ReadAllText(_filePath));
    }

    [Fact]
    public void LoadFile_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Bob", Balance = 50m });
        repo.SaveChanges();

        var reload = new JsonRepository<Account>(_filePath);
        var items = reload.GetAll().ToList();

        Assert.Single(items);
        Assert.Equal("Bob", items[0].OwnerName);
    }

    [Fact]
    public void ReloadData_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });
        repo.SaveChanges();

        var reload = new JsonRepository<Account>(_filePath);
        Assert.Single(reload.GetAll());
    }

    [Fact]
    public void MultipleOperations_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 100m });
        repo.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 50m });
        repo.SaveChanges();

        var reload = new JsonRepository<Account>(_filePath);
        reload.Add(new Account { Id = 3, OwnerName = "Charlie", Balance = 200m });
        reload.SaveChanges();

        var final = new JsonRepository<Account>(_filePath);
        Assert.Equal(3, final.GetAll().Count());
    }

    [Fact]
    public void MissingFile_ShouldBeHandled()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);

        var repo = new JsonRepository<Account>(_filePath);

        Assert.Empty(repo.GetAll());
    }

    [Fact]
    public void InvalidJson_ShouldBeHandled()
    {
        File.WriteAllText(_filePath, "{ invalid json }");

        var repo = new JsonRepository<Account>(_filePath);

        Assert.Empty(repo.GetAll());
    }

    [Fact]
    public void PersistenceCycle_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Dana", Balance = 75m });
        repo.SaveChanges();

        var reload = new JsonRepository<Account>(_filePath);
        Assert.Single(reload.GetAll());

        reload.Add(new Account { Id = 2, OwnerName = "Eve", Balance = 40m });
        reload.SaveChanges();

        var final = new JsonRepository<Account>(_filePath);
        Assert.Equal(2, final.GetAll().Count());
    }

    [Fact]
    public void SequentialSave_ShouldWork()
    {
        var repo = new JsonRepository<Account>(_filePath);
        repo.Add(new Account { Id = 1, OwnerName = "Alice", Balance = 120m });
        repo.SaveChanges();

        var next = new JsonRepository<Account>(_filePath);
        next.Add(new Account { Id = 2, OwnerName = "Bob", Balance = 80m });
        next.SaveChanges();

        var final = new JsonRepository<Account>(_filePath);
        Assert.Equal(2, final.GetAll().Count());
    }
}
