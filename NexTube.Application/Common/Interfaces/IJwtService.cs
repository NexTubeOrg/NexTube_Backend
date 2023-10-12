namespace NexTube.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email, string[] roles);
    }
}
