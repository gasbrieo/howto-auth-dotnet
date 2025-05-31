using HowTo.Auth.Core.Entities;
using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Auth.Register;

public class RegisterUserUseCase(IUserRepository userRepository, ITokenService tokenService) : IRegisterUserUseCase
{
    public async Task<Result<RegisterUserResponse>> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
        };

        var result = await userRepository.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result.Error([.. result.Errors.Select(e => e.Description)]);

        var token = tokenService.GenerateToken(user);

        return new RegisterUserResponse
        {
            Token = token
        };
    }
}
