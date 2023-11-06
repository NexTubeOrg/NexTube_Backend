using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;

namespace NexTube.Application.CQRS.Identity.Users.Commands.ChangePassword {
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result> {
        private readonly IIdentityService _identityService;
        public ChangePasswordCommandHandler(IIdentityService identityService) {
            _identityService = identityService;
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {
            var result = await _identityService.ChangePasswordAsync(
                request.UserId, request.Password, request.NewPassword);

            return result;
        }
    }
}
