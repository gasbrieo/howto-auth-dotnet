using HowTo.Auth.Core.Entities;
using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Auth.Register;
using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Tests.Auth.Register;

public class RegisterUserUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly RegisterUserUseCase _registerUserUseCase;

    public RegisterUserUseCaseTests()
    {
        _userRepositoryMock = new();
        _tokenServiceMock = new();
        _registerUserUseCase = new(_userRepositoryMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserCreationFails_ShouldReturnError()
    {
        // Arrange
        var request = new RegisterUserRequest
        {
            Email = "test@example.com",
            Password = "Rand0m$Pass123"
        };

        var identityErrors = new List<IdentityError>
        {
            new() { Description = "Password is too weak." }
        };

        _userRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<ApplicationUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Failed([.. identityErrors]));

        // Act
        var result = await _registerUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Error, result.Status);
        Assert.Contains("Password is too weak.", result.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserCreationSucceeds_ShouldReturnOk()
    {
        // Arrange
        var request = new RegisterUserRequest
        {
            Email = "test@example.com",
            Password = "Rand0m$Pass123"
        };

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var expectedToken = "generated-jwt-token";

        _userRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<ApplicationUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Success);

        _tokenServiceMock
            .Setup(service => service.GenerateToken(It.IsAny<ApplicationUser>()))
            .Returns(expectedToken);

        // Act
        var result = await _registerUserUseCase.ExecuteAsync(request);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedToken, result.Value.Token);
    }
}
