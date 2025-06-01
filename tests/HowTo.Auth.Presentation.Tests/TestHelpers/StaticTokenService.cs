namespace HowTo.Auth.Presentation.Tests.TestHelpers;

public static class StaticTokenService
{
    public const string Issuer = "ed8fb738-7a57-4533-a890-5339b5d01ed8";
    public const string Audience = "88ecf2f0-f493-4d2f-a8ed-4672292249e5";
    public const string SecretKey = "3c0fe9a7b6344daf988094ebf31e6936";

    public static string GenerateToken(string username = "test-user", string role = "test-role")
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, username),
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
