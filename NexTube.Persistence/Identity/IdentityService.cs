using Ardalis.GuardClauses;
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
      


        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        
        public async Task<(Result Result, int UserId)> CreateUserAsync(
            string password, string email, string firstName, string lastName,string nickname,string description)
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
                Nickname = nickname,
                Description = description
            };

            var result = await _userManager.CreateAsync(user, password);
            return (result.ToApplicationResult(), user.Id);
        }
        public async Task<(Result Result, int UserId)> UdateUserAsync(int userId, string nickname, string description)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException("User", userId.ToString());
            }
            user.Id = userId;
            user.Nickname = nickname;
            user.Description = description;


            var result = await _userManager.UpdateAsync(user);
            return (result.ToApplicationResult(), user.Id);

        }

    }
}
