using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Users.GetCurrentUser;

public interface IGetCurrentUserUseCase
{
    Task<Result<GetCurrentUserResponse>> ExecuteAsync(GetCurrentUserRequest request, CancellationToken cancellationToken = default);
}
