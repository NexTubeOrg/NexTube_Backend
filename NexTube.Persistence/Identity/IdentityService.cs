using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Persistence.Identity;
using WebShop.Application.Common.Exceptions;

namespace NexTube.Persistance.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        
        public async Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                throw new AlreadyExistsException(email, "User is already exist");
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
            };

            var result = await _userManager.CreateAsync(user, password);
            return (result.ToApplicationResult(), user.Id);
        }
    }
}
