using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using System.Security.Authentication;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInUser {
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserCommandResult> {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public SignInUserCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService) {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<SignInUserCommandResult> Handle(SignInUserCommand request, CancellationToken cancellationToken) {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new InvalidCredentialException("Wrong login or password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid) {
                await _userManager.AccessFailedAsync(user);
                throw new InvalidCredentialException("Wrong login or password");
            }

            

            var userRoles = await _userManager.GetRolesAsync(user);

            var userLookup = new UserLookup() {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ChannelPhoto = user.ChannelPhotoFileId.ToString(),
                Roles = userRoles
            };

            return new SignInUserCommandResult() {
                Result = Result.Success(),
                Token = _jwtService.GenerateToken(user.Id, userLookup),
                User = userLookup
            };
        }
    }
}
