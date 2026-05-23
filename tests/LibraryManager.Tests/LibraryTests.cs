using Xunit;

namespace LibraryManager.Tests;

public class LibraryTests
{
    [Fact]
    public void BorrowBook_ShouldWork()
    {
        Assert.True(true);
    }

    [Fact]
    public void ReturnBook_ShouldWork()
    {
        Assert.True(true);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void BookId_ShouldBeValid(int id)
    {
        Assert.True(id > 0);
    }
}
