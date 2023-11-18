using NexTube.Application.Models.Lookups;

namespace NexTube.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, UserLookup user);
    }
}
