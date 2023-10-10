using NexTube.Application.Common.Models;

namespace NexTube.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName);
    }
}
