using HowTo.Auth.Core.Entities;
using HowTo.Auth.Infrastructure.Data.Repositories;
using HowTo.Auth.Infrastructure.Tests.TestHelpers;

namespace HowTo.Auth.Infrastructure.Tests.Repositories;

public class UserRepositoryTests(IntegrationTestFixture fixture) : IClassFixture<IntegrationTestFixture>, IDisposable
{
    private readonly UserRepository _repository = new(fixture.UserManager);

    [Fact]
    public async Task GetByIdAsync_WhenIdDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();

        // Act
        var user = await _repository.GetByIdAsync(id);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task GetByIdAsync_WhenIdExists_ShouldReturnUser()
    {
        // Arrange
        var id = Guid.NewGuid();

        await _repository.CreateAsync(new ApplicationUser
        {
            Id = id,
            Email = "existent@email.com",
            UserName = "existent@email.com",
        }, "Rand0m$Pass123");

        // Act
        var user = await _repository.GetByIdAsync(id.ToString());

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenEmailDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var email = "nonexistent@email.com";

        // Act
        var user = await _repository.GetByEmailAsync(email);

        // Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task GetByEmailAsync_WhenEmailExists_ShouldReturnUser()
    {
        // Arrange
        var email = "existent@email.com";

        await _repository.CreateAsync(new ApplicationUser
        {
            Email = email,
            UserName = email,
        }, "Rand0m$Pass123");

        // Act
        var user = await _repository.GetByEmailAsync(email);

        // Assert
        Assert.NotNull(user);
    }

    [Fact]
    public async Task CheckPasswordAsync_WhenPasswordMatches_ShouldReturnTrue()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
        };

        await _repository.CreateAsync(user, password);

        // Act
        var matchPassword = await _repository.CheckPasswordAsync(user, password);

        // Assert
        Assert.True(matchPassword);
    }

    [Fact]
    public async Task CheckPasswordAsync_WhenPasswordDoesNotMatch_ShouldReturnFalse()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        var user = new ApplicationUser
        {
            Email = email,
            UserName = email,
        };

        await _repository.CreateAsync(user, password);

        // Act
        var matchPassword = await _repository.CheckPasswordAsync(user, "dontmatch");

        // Assert
        Assert.False(matchPassword);
    }

    [Fact]
    public async Task CreateAsync_WhenCalled_ShouldReturnSuccessfulResult()
    {
        // Arrange
        var email = "existent@email.com";
        var password = "Rand0m$Pass123";

        // Act
        var identityResult = await _repository.CreateAsync(new ApplicationUser
        {
            Email = email,
            UserName = email,
        }, password);

        // Assert
        Assert.NotNull(identityResult);
        Assert.True(identityResult.Succeeded);
    }

    public void Dispose()
    {
        fixture.ReinitializeDatabase();
        GC.SuppressFinalize(this);
    }
}
