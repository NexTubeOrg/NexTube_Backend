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
using IHttpClientFactory = NexTube.Application.Common.Interfaces.IHttpClientFactory;

namespace NexTube.Persistence.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly IMailService mailService;
        private readonly IPhotoService photoService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IProviderAuthManager providerAuthManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jwtService,
            IProviderAuthManager providerAuthManager,
            IMailService mailService,
            IPhotoService photoService,
            IHttpClientFactory httpClientFactory)
        {

            _userManager = userManager;
            this.roleManager = roleManager;
            this.jwtService = jwtService;
            this.providerAuthManager = providerAuthManager;
            this.mailService = mailService;
            this.photoService = photoService;
            this.httpClientFactory = httpClientFactory;
        }

        private async Task<(Result Result, int UserId)> VerifyUserExist(UserLookup userInfo)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(userInfo.Email ?? "");
            if (user != null)
            {
                userInfo.ChannelPhoto = user.ChannelPhotoFileId.ToString();
                return (Result.Success(), user.Id);
            }

            Guid photoFileId = default;

            // if provider provided user photo
            if (userInfo.ChannelPhoto is not null)
            {
                var http = httpClientFactory.CreateClient();
                var photoStream = await http.GetStreamAsync(userInfo.ChannelPhoto);
                Guid.TryParse((await photoService.UploadPhoto(photoStream)).PhotoId, out photoFileId);
            }

            var result = await CreateUserAsync(userInfo.Email ?? "", userInfo.FirstName ?? "", userInfo.LastName ?? "", photoFileId);
            userInfo.ChannelPhoto = photoFileId.ToString();

            return (result.Result, result.User.Id);
        }


        public async Task<Result> AddToRoleAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            return await AddToRoleAsync(user, roleName);
        }
        private async Task<Result> AddToRoleAsync(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.ToApplicationResult();

        }
        public async Task<Result> CreateRoleAsync(string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) != null)
            {
                throw new AlreadyExistsException(roleName, nameof(ApplicationRole));
            }

            var result = await roleManager.CreateAsync
                (
                    new ApplicationRole()
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    }
                );
            return result.ToApplicationResult();
        }
      
        public async Task<(Result Result, UserLookup User)> CreateUserAsync( string password, string email, string firstName, string lastName, Guid channelPhotoFileId)
        {

            var result = await CreateUserAsync(email, firstName, lastName, channelPhotoFileId);
            await _userManager.AddPasswordAsync(result.User, password);

            return (result.Result, new UserLookup()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserId = result.User.Id,
                Roles = (await GetUserRolesAsync(result.User.Id)).Roles,
                ChannelPhoto = result.User.ChannelPhotoFileId.ToString()
            });
        }
        public async Task<(Result Result, ApplicationUser User)> CreateUserAsync( string email, string firstName, string lastName, Guid photoFileId)
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

        public async Task<(Result Result, IList<string> Roles)> GetUserRolesAsync(int userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            var roles = await _userManager.GetRolesAsync(user);

            return (Result.Success(), roles);
        }

        public async Task<(Result Result, string? Token, UserLookup? User)> SignInAsync(string email, string password)
        {
            (Result Result, string? Token, UserLookup? User) failture =
                (Result.Failure(new[] {
                        "Wrong login or password"
                    }), null, null);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return failture;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                await _userManager.AccessFailedAsync(user);
                return failture;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var userLookup = new UserLookup()
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ChannelPhoto = user.ChannelPhotoFileId.ToString(),
                Roles = userRoles
            };

            return (Result.Success(),
                    jwtService.GenerateToken(user.Id, userLookup),
                    userLookup);
        }

        public async Task<(Result Result, string? Token, UserLookup? User)> SignInOAuthAsync(string provider, string providerToken)
        {
            // get user info from token, issued by provider
            var tokenVerificationResult = await providerAuthManager.AuthenticateAsync(provider, providerToken);

            // verify that user exist in database
            var existenceVerificationResult = await VerifyUserExist(tokenVerificationResult.User);

            // get user roles
            tokenVerificationResult.User.Roles = (await GetUserRolesAsync(existenceVerificationResult.UserId)).Roles;

            // generate application token
            var token = jwtService.GenerateToken(
                existenceVerificationResult.UserId,
                tokenVerificationResult.User);

            return (Result.Success(), token, tokenVerificationResult.User);
        }

        public async Task<(Result Result, int? UserId)> GetUserIdByEmailAsync(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundException(email, nameof(ApplicationUser));

            return (Result.Success(), user.Id);
        }

        public async Task<Result> RecoverAsync(
           string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Success();
            }
            var newPassword = mailService.GeneratePassword(10);
            //await _userManager.ResetPasswordAsync(user,await _userManager.GeneratePasswordResetTokenAsync(user),newPassword);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);
            await mailService.SendMailAsync("<h1>Hello, we have recently reseted your password to " + newPassword + " <br>Please change it in your profile settings if required.</h1>", email);

            return Result.Success();
        }

        public async Task<Result> ChangePasswordAsync(
         int userId, string password, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result.Failure(new[] {
                        "User not found!"
                    });
            }
            var res = await _userManager.ChangePasswordAsync(user, password, newPassword);

            return res.ToApplicationResult();
        }

        public async Task<(Result Result, ApplicationUser User)> GetUserByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new NotFoundException(userId.ToString(), nameof(ApplicationUser));

            return (Result.Success(), user);
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