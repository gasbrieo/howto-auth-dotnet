using HowTo.Auth.Core.Entities;
using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Common;
using HowTo.Auth.UseCases.Me.GetCurrentUser;

namespace HowTo.Auth.UseCases.Tests.Me.GetCurrentUser;

public class GetCurrentUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly GetCurrentUserUseCase _getCurrentUserUseCase;

    public GetCurrentUserUseCaseTests()
    {
        _userRepositoryMock = new();
        _getCurrentUserUseCase = new(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserDoesNotExist_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new GetCurrentUserRequest
        {
            UserId = Guid.NewGuid().ToString()
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.UserId))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _getCurrentUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Unauthorized, result.Status);
        Assert.Contains("The user is no longer present in the system.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserDoesExist_ShouldReturnOk()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };

        var request = new GetCurrentUserRequest
        {
            UserId = user.Id.ToString()
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(request.UserId))
            .ReturnsAsync(user);

        // Act
        var result = await _getCurrentUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(user.Id, result.Value.Id);
        Assert.Equal(user.Email, result.Value.Email);
    }
}
