using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.BanUser
{
    public class BanUserCommnadHandler : IRequestHandler<BanUserCommand, BanUserCommandResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public BanUserCommnadHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }
        public async Task<BanUserCommandResult> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
                var result = await _identityService.BanUserAsync(request.UserId);

            return new BanUserCommandResult() {
                Result = result
            };
        }
    }
}
