using HowTo.Auth.Core.Entities;
using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Auth.Login;
using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Tests.Auth.Login;

public class LoginUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly LoginUserUseCase _loginUserUseCase;

    public LoginUserUseCaseTests()
    {
        _userRepositoryMock = new();
        _tokenServiceMock = new();
        _loginUserUseCase = new(_userRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserDoesNotExist_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new LoginUserRequest
        {
            Email = "nonexistent@example.com",
            Password = "password123"
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Unauthorized, result.Status);
        Assert.Contains("Invalid email or password.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenPasswordIsInvalid_ShouldReturnUnauthorized()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };

        var request = new LoginUserRequest
        {
            Email = user.Email,
            Password = "wrongpassword"
        };

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        _userRepositoryMock
            .Setup(repo => repo.CheckPasswordAsync(user, request.Password))
            .ReturnsAsync(false);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Unauthorized, result.Status);
        Assert.Contains("Invalid email or password.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenCredentialsAreValid_ShouldReturnOk()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };

        var request = new LoginUserRequest
        {
            Email = user.Email,
            Password = "correctpassword"
        };

        var expectedToken = "generated-jwt-token";

        _userRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        _userRepositoryMock
            .Setup(repo => repo.CheckPasswordAsync(user, request.Password))
            .ReturnsAsync(true);

        _tokenServiceMock
            .Setup(service => service.GenerateToken(user))
            .Returns(expectedToken);

        // Act
        var result = await _loginUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedToken, result.Value.Token);
    }
}
