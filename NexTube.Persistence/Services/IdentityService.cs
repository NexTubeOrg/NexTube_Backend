using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using WebShop.Application.Common.Exceptions;
using WebShop.Domain.Constants;
using NexTube.Domain.Entities;
using NexTube.Domain.Entities.Abstract;
using NexTube.Persistence.Identity;
using NexTube.Application.Models.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace NexTube.Persistence.Services {
    public class IdentityService : IIdentityService {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager) {
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        private async Task<Result> AddToRoleAsync(ApplicationUser user, string roleName) {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.ToApplicationResult();

        }

        public async Task<Result> AddToRoleAsync(int userId, string roleName) {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            return await AddToRoleAsync(user, roleName);
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
        public async Task<(Result Result, UserLookup User)> CreateUserAsync(string password, string email, string firstName, string lastName, Guid channelPhotoFileId) {

            var result = await CreateUserAsync(email, firstName, lastName, channelPhotoFileId);
            await _userManager.AddPasswordAsync(result.User, password);
            
            return (result.Result, new UserLookup() {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserId = result.User.Id,
                Roles = (await GetUserRolesAsync(result.User.Id)).Roles,
                ChannelPhoto = result.User.ChannelPhotoFileId.ToString()
            });
        }
        public async Task<(Result Result, ApplicationUser User)> CreateUserAsync(string email, string firstName, string lastName, Guid photoFileId)
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
                ChannelPhotoFileId = photoFileId
            };

            var result = await _userManager.CreateAsync(user);

            await AddToRoleAsync(user, Roles.Unverified);

            return (result.ToApplicationResult(), user);
        }
        public async Task<(Result Result, ApplicationUser User)> CreateVerifiedUserAsync(string email, string firstName, string lastName, Guid photoFileId)
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
                ChannelPhotoFileId = photoFileId
            };

            var result = await _userManager.CreateAsync(user);

            await AddToRoleAsync(user, Roles.User);

            return (result.ToApplicationResult(), user);
        }
        public async Task<(Result Result, IList<string> Roles)> GetUserRolesAsync(int userId) {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            var roles = await _userManager.GetRolesAsync(user);

            return (Result.Success(), roles);
        }
        public async Task<(Result Result, UserLookup userLookup)> GetUserLookupAsync(int userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));
            var res = new UserLookup()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = userId,
                Roles = (await GetUserRolesAsync(userId)).Roles,
                ChannelPhoto = user.ChannelPhotoFileId.ToString()
            };
            return (Result.Success(), res);
           }
    }
}
