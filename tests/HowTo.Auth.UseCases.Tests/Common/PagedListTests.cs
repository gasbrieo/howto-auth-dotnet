using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Tests.Common;

public class PagedListTests
{
    [Fact]
    public void Constructor_WhenCalledWithValidParameters_ShouldSetProperties()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var totalItems = 100;
        var items = new List<string>();

        // Act
        var pagedList = new PagedList<string>(pageNumber, pageSize, totalItems, items);

        // Assert
        Assert.Equal(pageNumber, pagedList.PageNumber);
        Assert.Equal(pageSize, pagedList.PageSize);
        Assert.Equal(totalItems, pagedList.TotalItems);
        Assert.Equal(10, pagedList.TotalPages);
        Assert.Equal(items, pagedList.Items);
    }
}