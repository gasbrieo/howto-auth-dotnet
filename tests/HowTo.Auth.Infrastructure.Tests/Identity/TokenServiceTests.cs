using HowTo.Auth.Core.Entities;
using HowTo.Auth.Infrastructure.Identity;

namespace HowTo.Auth.Infrastructure.Tests.Identity;

public class TokenServiceTests
{
    private readonly string TestIssuer = "TestIssuer";
    private readonly string TestAudience = "TestAudience";
    private readonly string TestKey = Guid.NewGuid().ToString();

    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        Environment.SetEnvironmentVariable("JWT_KEY", TestKey);

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "Jwt:Issuer", TestIssuer },
                { "Jwt:Audience", TestAudience }
            })
            .Build();

        _tokenService = new TokenService(configuration);
    }

    [Fact]
    public void GenerateToken_ShouldReturnValidJwtToken()
    {
        // Arrange
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "testuser",
            Email = "testuser@example.com"
        };

        // Act
        var token = _tokenService.GenerateToken(user);

        // Assert
        Assert.NotNull(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(TestKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = TestIssuer,
            ValidAudience = TestAudience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        Assert.NotNull(principal);
        Assert.IsType<JwtSecurityToken>(validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        Assert.Equal(TestIssuer, jwtToken.Issuer);
        Assert.Equal(TestAudience, jwtToken.Audiences.First());
        Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString());
        Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Name && c.Value == user.UserName);
        Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Email && c.Value == user.Email);
    }
}

