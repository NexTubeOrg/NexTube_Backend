using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;

namespace NexTube.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, UserLookup user);
    }
}
