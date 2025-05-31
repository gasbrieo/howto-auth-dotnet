using HowTo.Auth.Core.Interfaces;
using HowTo.Auth.UseCases.Common;

namespace HowTo.Auth.UseCases.Me.GetCurrentUser;

public class GetCurrentUserUseCase(IUserRepository userRepository) : IGetCurrentUserUseCase
{
    public async Task<Result<GetCurrentUserResponse>> ExecuteAsync(GetCurrentUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user == null)
            return Result.Unauthorized("The user is no longer present in the system.");

        return new GetCurrentUserResponse
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty
        };
    }
}
