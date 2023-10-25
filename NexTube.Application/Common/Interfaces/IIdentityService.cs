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

        /// <summary>
        /// Ensure that user exist in database after execution
        /// If it is already exist - just return it`s ID.
        /// Otherwise - create and write to database
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="providerToken"></param>
        /// <returns></returns>
        Task<(Result Result, string? Token, UserLookup? User)> SignInOAuthAsync(
            string provider, string providerToken);

        Task<Result> CreateRoleAsync(
            string roleName);
        Task<Result> AddToRoleAsync(
            int userId, string roleName);

        Task<(Result Result, IList<string> Roles)> GetUserRolesAsync(
            int userId);

        Task<(Result Result, int? UserId)> GetUserIdByEmailAsync(string email);
    }
}
