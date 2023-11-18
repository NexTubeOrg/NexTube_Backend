using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInWithProvider {
    public class SignInWithProviderCommandHandler : IRequestHandler<SignInWithProviderCommand, SignInWithProviderCommandResult>
    {
        private readonly IIdentityService _identityService;
        public SignInWithProviderCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<SignInWithProviderCommandResult> Handle(SignInWithProviderCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SignInOAuthAsync(request.Provider, request.ProviderToken);
            
            SignInWithProviderCommandResult dto = new SignInWithProviderCommandResult() {
                Result = result.Result,
                Token = result.Token,
                User = result.User
            };

            return dto;
        }
    }
}
