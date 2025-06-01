using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Auth.Login;

public class LoginUserUseCase(IUserRepository userRepository, ITokenService tokenService) : ILoginUserUseCase
{
    public async Task<Result<LoginUserResponse>> ExecuteAsync(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return Result.Unauthorized("Invalid email or password.");

        var isValid = await userRepository.CheckPasswordAsync(user, request.Password);

        if (!isValid)
            return Result.Unauthorized("Invalid email or password.");

        var token = tokenService.GenerateToken(user);

        return new LoginUserResponse
        {
            Token = token
        };
    }
}
