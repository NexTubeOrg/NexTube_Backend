using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.Recover {

    public class RecoverCommandHandler : IRequestHandler<RecoverCommand, Result> {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;

        public RecoverCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMailService mailService) {
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<Result> Handle(RecoverCommand request, CancellationToken cancellationToken) {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) {
                return Result.Success();
            }
            var newPassword = _mailService.GeneratePassword(10);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, newPassword);
            await _mailService.SendMailAsync("<h1>Hello, we have recently reseted your password to " + newPassword + " <br>Please change it in your profile settings if required.</h1>", request.Email);

            return Result.Success();
        }
    }
}
