using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInUser   
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, SignInUserCommandResult>
    {
        private readonly IIdentityService _identityService;
        public SignInUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<SignInUserCommandResult> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SignInAsync(request.Email, request.Password);
            
            SignInUserCommandResult dto = new SignInUserCommandResult() {
                Result = result.Result,
                Token = result.Token,
                User = result.User
            };

            return dto;
        }
    }
}
