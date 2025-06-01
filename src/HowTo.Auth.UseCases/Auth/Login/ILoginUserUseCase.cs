using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Auth.Login;

public interface ILoginUserUseCase
{
    Task<Result<LoginUserResponse>> ExecuteAsync(LoginUserRequest request, CancellationToken cancellationToken = default);
}
