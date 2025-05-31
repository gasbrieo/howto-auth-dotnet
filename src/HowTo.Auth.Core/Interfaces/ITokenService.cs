using HowTo.Auth.Core.Entities;

namespace HowTo.Auth.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
}
