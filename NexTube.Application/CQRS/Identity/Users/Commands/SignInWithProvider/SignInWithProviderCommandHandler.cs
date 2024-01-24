using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using System.Net.Http;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInWithProvider {
    public class SignInWithProviderCommandHandler : IRequestHandler<SignInWithProviderCommand, SignInWithProviderCommandResult> {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IPhotoService _photoService;
        private readonly IProviderAuthManager _providerAuthManager;
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identityService;

        public SignInWithProviderCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory httpClientFactory,
            IPhotoService photoService,
            IProviderAuthManager providerAuthManager,
            IJwtService jwtService,
            IIdentityService identityService) {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _photoService = photoService;
            _providerAuthManager = providerAuthManager;
            _jwtService = jwtService;
            _identityService = identityService;
        }

        private async Task<(Result Result, ApplicationUser User)> VerifyUserExist(UserLookup userInfo) {
            ApplicationUser? user = await _userManager.FindByEmailAsync(userInfo.Email ?? "");

            if (user != null) {
                userInfo.ChannelPhoto = user.ChannelPhotoFileId.ToString();
                return (Result.Success(), user);
            }


            Guid photoFileId = default;

            // if provider provided user photo
            if (userInfo.ChannelPhoto is not null) {
                var http = _httpClientFactory.CreateClient();
                var photoStream = await http.GetStreamAsync(userInfo.ChannelPhoto);
                Guid.TryParse((await _photoService.UploadPhoto(photoStream)).PhotoId, out photoFileId);
            }

            var result = await _identityService.CreateVerifiedUserAsync(userInfo.Email ?? "", userInfo.FirstName ?? "", userInfo.LastName ?? "", photoFileId);
            userInfo.ChannelPhoto = photoFileId.ToString();

            return (result.Result, result.User);
        }

        public async Task<SignInWithProviderCommandResult> Handle(SignInWithProviderCommand request, CancellationToken cancellationToken) {
            // get user info from token, issued by provider
            var tokenVerificationResult = await _providerAuthManager.AuthenticateAsync(request.Provider, request.ProviderToken);

            // verify that user exist in database
            var existenceVerificationResult = await VerifyUserExist(tokenVerificationResult.User);

            // get user roles
            tokenVerificationResult.User.Roles = (await _identityService.GetUserRolesAsync(existenceVerificationResult.User.Id)).Roles;

            // generate application token
            var token = _jwtService.GenerateToken(
                existenceVerificationResult.User.Id,
                new UserLookup() {
                    ChannelPhoto = existenceVerificationResult.User.ChannelPhotoFileId.ToString(),
                    Email = existenceVerificationResult.User.Email,
                    FirstName = existenceVerificationResult.User.FirstName,
                    LastName = existenceVerificationResult.User.LastName,
                    UserId = existenceVerificationResult.User.Id,
                    Roles = tokenVerificationResult.User.Roles
                });

            return new SignInWithProviderCommandResult() {
                Result = Result.Success(),
                Token = token,
                User = tokenVerificationResult.User
            };
        }
    }
}
