using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using System.Data;
using WebShop.Application.Common.Exceptions;
using WebShop.Domain.Constants;

namespace NexTube.Persistence.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            this.jwtService = jwtService;
        }

        public async Task<Result> AddToRoleAsync(int userId, string roleName) {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            return await AddToRoleAsync(user, roleName);
        }
        private async Task<Result> AddToRoleAsync(ApplicationUser user, string roleName) {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.ToApplicationResult();

        }
        public async Task<Result> CreateRoleAsync(string roleName) {
            if (await roleManager.FindByNameAsync(roleName) != null) {
                throw new AlreadyExistsException(roleName, nameof(ApplicationRole));
            }

            var result = await roleManager.CreateAsync
                (
                    new ApplicationRole() {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    }
                );
            return result.ToApplicationResult();
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

            await AddToRoleAsync(user, Roles.User);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<(Result Result, string? Token, UserLookup? User)> SignInAsync(string email, string password) {
            (Result Result, string? Token, UserLookup? User) failture = (Result.Failure(new[] {
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

            var userLookup = new UserLookup() {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = userRoles
            };

            return (
                    Result.Success(),
                    jwtService.GenerateToken(user.Id, userLookup),
                    userLookup
                    );
        }
    }
}
