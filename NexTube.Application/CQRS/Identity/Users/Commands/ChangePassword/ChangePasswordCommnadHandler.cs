using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword {
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result> {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangePasswordCommandHandler(
            UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) {
                return Result.Failure(new[] {
                        "User not found!"
                    });
            }
            var res = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

            return Result.Success();
        }
    }
}
