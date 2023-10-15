using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using WebShop.Application.Common.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NexTube.Persistence.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService jwtService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            this.jwtService = jwtService;
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

        public async Task<(Result Result, string? Token, string? FirstName)> SignInAsync(string email, string password) {
            (Result Result, string? Token, string? FirstName) failture = (Result.Failure(new[] {
                        "Wrong login or password"
                    }), null, null);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                return failture;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid) {
                var res = await _userManager.AccessFailedAsync(user);
                return failture;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return (
                    Result.Success(),
                    jwtService.GenerateToken(user.Id, email, userRoles.ToArray()),
                    user.FirstName);
        }
    }
}
