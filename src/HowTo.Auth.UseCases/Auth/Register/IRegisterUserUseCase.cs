using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Auth.Register;

public interface IRegisterUserUseCase
{
    Task<Result<RegisterUserResponse>> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
}
