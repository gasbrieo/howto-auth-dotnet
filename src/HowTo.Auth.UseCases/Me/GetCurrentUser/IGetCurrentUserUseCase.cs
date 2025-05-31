using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Me.GetCurrentUser;

public interface IGetCurrentUserUseCase
{
    Task<Result<GetCurrentUserResponse>> ExecuteAsync(GetCurrentUserRequest request, CancellationToken cancellationToken = default);
}
