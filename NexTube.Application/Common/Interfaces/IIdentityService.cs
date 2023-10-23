using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;

namespace NexTube.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName);

        Task<(Result Result, string? Token, UserLookup? User)> SignInAsync(
            string email, string password);

        Task<Result> CreateRoleAsync(
            string roleName);
        Task<Result> AddToRoleAsync(
            int userId, string roleName);
    }
}
