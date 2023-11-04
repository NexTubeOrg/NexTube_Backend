using NexTube.Application.Common.Models;

namespace NexTube.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, int UserId)> UdateUserAsync(
         int userId, string nickname, string description);
        Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName,string nickname ,string description);
    }
}
